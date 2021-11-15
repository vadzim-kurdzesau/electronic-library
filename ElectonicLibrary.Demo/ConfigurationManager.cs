using System.IO;
using Microsoft.Extensions.Configuration;

namespace ElectronicLibrary.Demo
{
    public class ConfigurationManager
    {
        public ConfigurationManager()
            => this.Configuration = new ConfigurationBuilder().SetBasePath(GetPathToConfigurationFile())
                .AddJsonFile("appsettings.json").Build();

        public IConfigurationRoot Configuration { get; }

        private static string GetPathToConfigurationFile()
            => GetPathToHigherDirectory(GetPathToHigherDirectory((Directory.GetCurrentDirectory())));

        private static string GetPathToHigherDirectory(string path)
            => Directory.GetParent(path).Parent.FullName;
    }
}
