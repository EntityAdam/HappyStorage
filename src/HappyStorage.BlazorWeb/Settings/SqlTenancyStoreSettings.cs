using HappyStorage.SqlStorage;
using Microsoft.Extensions.Configuration;

namespace HappyStorage.BlazorWeb.Settings
{
    public class SqlTenancyStoreSettings : ISqlTenancyStoreSettings
    {
        private readonly IConfiguration configuration;

        public SqlTenancyStoreSettings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetConnectionString()
        {
            return configuration.GetConnectionString("SqlUnitStore");
        }
    }
}