using Dapper;
using HappyStorage.Core;
using HappyStorage.Core.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace HappyStorage.SqlStorage
{
    public class SqlTenancyStore : ITenancyStore
    {
        private readonly ISqlTenancyStoreSettings sqlTenancyStoreSettings;

        public SqlTenancyStore(ISqlTenancyStoreSettings sqlTenancyStoreSettings) =>
            this.sqlTenancyStoreSettings = sqlTenancyStoreSettings ?? throw new ArgumentNullException(nameof(sqlTenancyStoreSettings));

        public void Create(string unitNumber, string customerNumber, DateTime reservationDate, decimal amountPaid)
        {
            UseConnection(con =>
            {
                const string sql =
                    @"INSERT INTO [dbo].[Tenants]
						([UnitNumber]
						,[CustomerNumber]
						,[ReservationDate]
						,[AmountPaid])
					VALUES
						(@UnitNumber
						,@CustomerNumber
						,@ReservationDate
						,@AmountPaid)";
                var parameters = new
                {
                    UnitNumber = unitNumber,
                    CustomerNumber = customerNumber,
                    ReservationDate = reservationDate,
                    AmountPaid = amountPaid
                };
                con.Execute(sql, parameters);
            });
        }

        public void Delete(string unitNumber, string customerNumber)
        {
            UseConnection(con =>
            {
                const string sql =
                    @"DELETE [Tenants] WHERE UnitNumber = @UnitNumber AND CustomerNumber = @CustomerNumber";
                var parameters = new
                {
                    UnitNumber = unitNumber,
                    CustomerNumber = customerNumber
                };
                con.Execute(sql, parameters);
            });
        }

        public IEnumerable<TenantLookup> GetCustomerUnits(string customerNumber)
        {
            return UseConnection(con =>
            {
                const string sql =
                        @"SELECT
							[UnitNumber],
							[ReservationDate],
							[AmountPaid]
						FROM [Tenants] WHERE CustomerNumber = @CustomerNumber";
                var parameters = new
                {
                    CustomerNumber = customerNumber
                };
                return con.Query<TenantLookup>(sql, parameters);
            });
        }

        public IEnumerable<string> ListOccupiedUnits()
        {
            return UseConnection(con =>
            {
                const string sql = @"SELECT [UnitNumber] FROM [Tenants]";
                return con.Query<string>(sql);
            });
        }

        public IEnumerable<TenantLookup> ListTenants()
        {
            return UseConnection(con =>
            {
                const string sql = 
                        @"SELECT 
                            [UnitNumber],
                            [CustomerNumber],
                            [AmountPaid],
                            [ReservationDate]
                        FROM [Tenants]";
                return con.Query<TenantLookup>(sql);
            });
        }

        public bool IsUnitNumberOccupied(string unitNumber)
        {
            return UseConnection(con =>
            {
                const string sql =
                    @"SELECT COUNT(*) FROM [Tenants] WHERE UnitNumber = @UnitNumber";
                var parameters = new
                {
                    UnitNumber = unitNumber
                };
                return con.ExecuteScalar<int>(sql, parameters) > 0;
            });
        }

        public void UpdateAmountPaid(string unitNumber, decimal amountToApply)
        {
            UseConnection(con =>
            {
                const string sql =
                    @"UPDATE [Tenants] SET AmountPaid =+ amountToApply WHERE UnitNumber = @UnitNumber";
                var parameters = new
                {
                    AmountPaid = amountToApply,
                    UnitNumber = unitNumber
                };
                return con.ExecuteScalar<int>(sql, parameters) > 0;
            });
        }

        private void UseConnection(Action<SqlConnection> action)
        {
            using (var con = new SqlConnection(sqlTenancyStoreSettings.GetConnectionString()))
            {
                con.Open();
                action(con);
            }
        }

        private T UseConnection<T>(Func<SqlConnection, T> func)
        {
            using (var con = new SqlConnection(sqlTenancyStoreSettings.GetConnectionString()))
            {
                con.Open();
                return func(con);
            }
        }
    }
}