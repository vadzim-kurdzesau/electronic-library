using System.IO;
using Microsoft.Extensions.Configuration;

namespace ElectronicLibrary.Tests
{
    class ConfigurationManager
    {
        private static IConfigurationRoot _config;

        public static string ConnectionString
        {
            get
            {
                if (_config is null)
                {
                    _config = GetConfig();
                }

                return _config.GetSection("connectionStrings")["testDB"];
            }
        }

        private static IConfigurationRoot GetConfig()
        {
            if (_config == null)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.tests.json");
                _config = builder.Build();
            }

            return _config;
        }
    }
}
