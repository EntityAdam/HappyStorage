using Dapper;
using HappyStorage.Core;
using HappyStorage.Core.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyStorage.SqlStorage
{
    public class SqlCustomerStore : ICustomerStore
    {
        private ISqlCustomerStoreSettings sqlCustomerStoreSettings;

        public SqlCustomerStore(ISqlCustomerStoreSettings sqlCustomerStoreSettings)
        {
            this.sqlCustomerStoreSettings = sqlCustomerStoreSettings ?? throw new ArgumentNullException(nameof(sqlCustomerStoreSettings));
        }

        public void Create(NewCustomer newCustomer)
        {
            UseConnection(con =>
            {
                const string sql =
                    @"INSERT INTO [dbo].[Customers]
						([CustomerNumber]
						,[FullName]
						,[Address])
					VALUES
						(@CustomerNumber
						,@FullName
						,@Address)";
                var parameters = new
                {
                    newCustomer.CustomerNumber,
                    newCustomer.FullName,
                    newCustomer.Address,
                };
                con.Execute(sql, parameters);
            });
        }

        public bool CustomerExists(string customerNumber)
        {
            return UseConnection(con =>
            {
                const string sql =
                    @"SELECT COUNT(*) FROM [Customers] WHERE CustomerNumber = @CustomerNumber";
                var parameters = new
                {
                    CustomerNumber = customerNumber
                };
                return con.ExecuteScalar<int>(sql, parameters) > 0;
            });
        }

        public void Delete(string customerNumber)
        {
            UseConnection(con =>
            {
                const string sql =
                    @"DELETE [Customers] WHERE CustomerNumber = @CustomerNumber";
                var parameters = new
                {
                    CustomerNumber = customerNumber
                };
                con.Execute(sql, parameters);
            });
        }

        public IEnumerable<CustomerLookup> ListCustomers()
        {
            return UseConnection(con =>
            {
                const string sql =
                    @"SELECT TOP 100 CustomerNumber, FullName FROM [Customers]";

                return con.Query<CustomerLookup>(sql);
            });
        }

        public NewCustomer GetCustomer(string customerNumber)
        {
            return UseConnection(con =>
            {
                const string sql =
                    @"SELECT TOP 1 CustomerNumber, FullName, Address FROM [Customers] WHERE CustomerNumber = @CustomerNumber";
                var parameters = new
                {
                    CustomerNumber = customerNumber
                };
                return con.Query<NewCustomer>(sql, parameters).FirstOrDefault();
            });
        }

        public void UpdateCustomer(NewCustomer newCustomerDetails)
        {
            UseConnection(con =>
            {
                const string sql =
                    @"UPDATE [dbo].[Customers]
                      SET
                        [FullName] = @FullName
					    ,[Address] = @Address
                    WHERE CustomerNumber = @CustomerNumber"
                    ;
                var parameters = new
                {
                    newCustomerDetails.CustomerNumber,
                    newCustomerDetails.FullName,
                    newCustomerDetails.Address,
                };
                con.Execute(sql, parameters);
            });
        }

        private void UseConnection(Action<SqlConnection> action)
        {
            using (var con = new SqlConnection(sqlCustomerStoreSettings.GetConnectionString()))
            {
                con.Open();
                action(con);
            }
        }

        private T UseConnection<T>(Func<SqlConnection, T> func)
        {
            using (var con = new SqlConnection(sqlCustomerStoreSettings.GetConnectionString()))
            {
                con.Open();
                return func(con);
            }
        }
    }
}