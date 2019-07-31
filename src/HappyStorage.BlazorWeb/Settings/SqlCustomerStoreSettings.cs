using HappyStorage.SqlStorage;
using Microsoft.Extensions.Configuration;

namespace HappyStorage.BlazorWeb.Settings
{
    public class SqlCustomerStoreSettings : ISqlCustomerStoreSettings
    {
        private readonly IConfiguration configuration;

        public SqlCustomerStoreSettings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetConnectionString()
        {
            return configuration.GetConnectionString("SqlUnitStore");
        }
    }
}