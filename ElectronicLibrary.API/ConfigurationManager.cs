using System.IO;
using Microsoft.Extensions.Configuration;

namespace ElectronicLibrary.API
{
    public class ConfigurationManager
    {
        private readonly IConfigurationRoot _config;

        public ConfigurationManager()
        {
            _config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            this.ConnectionString = this.GetConnectionString();
        }

        public string ConnectionString { get; }

        private string GetConnectionString()
        {
            return this._config.GetSection("ConnectionStrings")["DefaultConnection"];
        }
    }
}
