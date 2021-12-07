using Dapper;
using HappyStorage.Core;
using HappyStorage.Core.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyStorage.SqlStorage
{
    public class SqlUnitStore : IUnitStore
    {
        private readonly ISqlUnitStoreSettings sqlUnitStoreSettings;

        public SqlUnitStore(ISqlUnitStoreSettings sqlUnitStoreSettings) =>
            this.sqlUnitStoreSettings = sqlUnitStoreSettings ?? throw new ArgumentNullException(nameof(sqlUnitStoreSettings));

        public void Create(NewUnit newUnit)
        {
            UseConnection(con =>
            {
                const string sql =
                    @"INSERT INTO [dbo].[Units]
						([UnitNumber]
						,[Length]
						,[Width]
						,[Height]
						,[IsClimateControlled]
						,[IsVehicleAccessible]
						,[PricePerMonth])
					VALUES
						(@UnitNumber
						,@Length
						,@Width
						,@Height
						,@IsClimateControlled
						,@IsVehicleAccessible
						,@PricePerMonth)";
                var parameters = new
                {
                    newUnit.UnitNumber,
                    newUnit.Length,
                    newUnit.Width,
                    newUnit.Height,
                    newUnit.IsClimateControlled,
                    newUnit.IsVehicleAccessible,
                    newUnit.PricePerMonth
                };
                con.Execute(sql, parameters);
            });
        }

        public void Delete(string unitNumber)
        {
            UseConnection(con =>
            {
                const string sql =
                    @"DELETE [Units] WHERE UnitNumber = @UnitNumber";
                var parameters = new
                {
                    UnitNumber = unitNumber
                };
                con.Execute(sql, parameters);
            });
        }

        public decimal GetPricePerMonth(string unitNumber)
        {
            return UseConnection(con =>
            {
                const string sql =
                    @"SELECT PricePerMonth FROM [Units] WHERE UnitNumber = @UnitNumber";
                var parameters = new
                {
                    UnitNumber = unitNumber
                };
                return con.ExecuteScalar<decimal>(sql, parameters);
            });
        }

        public IEnumerable<AvailableUnit> SearchAvailableUnits(bool? isClimateControlled, bool? isVehicleAccessible, int? minimumCubicFeet)
        {
            return UseConnection(con =>
            {
                var sql = new StringBuilder();
                sql.Append("SELECT UnitNumber, PricePerMonth FROM [Units]");
                var whereStatementApplied = false;
                if (isClimateControlled.HasValue)
                {
                    if (whereStatementApplied)
                        sql.Append(" AND ");
                    else
                        sql.Append(" WHERE ");
                    whereStatementApplied = true;
                    sql.Append("IsClimateControlled = ");
                    sql.Append(GetBitString(isClimateControlled.Value));
                }
                if (isVehicleAccessible.HasValue)
                {
                    if (whereStatementApplied)
                        sql.Append(" AND ");
                    else
                        sql.Append(" WHERE ");
                    whereStatementApplied = true;
                    sql.Append("IsVehicleAccessible = ");
                    sql.Append(GetBitString(isVehicleAccessible.Value));
                }
                if (minimumCubicFeet.HasValue)
                {
                    if (whereStatementApplied)
                        sql.Append(" AND ");
                    else
                        sql.Append(" WHERE ");
                    whereStatementApplied = true;
                    sql.Append("(Length * Width * Height) >= ");
                    sql.Append(minimumCubicFeet.Value.ToString());
                }
                return con.Query<AvailableUnit>(sql.ToString());
            });
        }

        public bool UnitExists(string unitNumber)
        {
            return UseConnection(con =>
            {
                const string sql =
                    @"SELECT COUNT(*) FROM [Units] WHERE UnitNumber = @UnitNumber";
                var parameters = new
                {
                    UnitNumber = unitNumber
                };
                return con.ExecuteScalar<int>(sql, parameters) > 0;
            });
        }

        private void UseConnection(Action<SqlConnection> action)
        {
            using var con = new SqlConnection(sqlUnitStoreSettings.GetConnectionString());
            con.Open();
            action(con);
        }

        private T UseConnection<T>(Func<SqlConnection, T> func)
        {
            using var con = new SqlConnection(sqlUnitStoreSettings.GetConnectionString());
            con.Open();
            return func(con);
        }

        private string GetBitString(bool value) => value ? "1" : "0";
    }
}