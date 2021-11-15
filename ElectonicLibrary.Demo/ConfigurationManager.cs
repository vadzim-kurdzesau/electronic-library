using System.IO;
using Microsoft.Extensions.Configuration;

namespace ElectronicLibrary.Demo
{
    public class ConfigurationManager
    {
        public ConfigurationManager()
        {
            this.Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
        }

        public IConfigurationRoot Configuration { get; }
    }
}
