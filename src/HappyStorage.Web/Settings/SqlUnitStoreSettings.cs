using HappyStorage.SqlStorage;
using Microsoft.Extensions.Configuration;

namespace HappyStorage.Web.Settings
{
    public class SqlUnitStoreSettings : ISqlUnitStoreSettings
    {
        private readonly IConfiguration configuration;

        public SqlUnitStoreSettings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GetConnectionString()
        {
            return configuration.GetConnectionString("SqlUnitStore");
        }
    }
}
