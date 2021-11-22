using System;
using System.IO;
using System.Linq;
using ElectronicLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace ElectronicLibrary.Demo
{
    internal class Program
    {
        private static IConfigurationRoot _config;

        private static void Main(string[] args)
        {
            var connectionString = GetConfig().GetSection("connectionStrings")["mainDB"];

            using var service = new ElectronicLibraryService(connectionString);
            var readerRepositoryCities = service.CitiesRepository.Cities;
            Console.WriteLine(string.Join(", ", readerRepositoryCities.Select(c => c.Name).ToList()));
        }

        private static IConfigurationRoot GetConfig()
        {
            if (_config == null)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
                _config = builder.Build();
            } 
            return _config;
        }
    }
}
