using System;

namespace ElectronicLibrary.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConfigurationManager configurationManager = new ConfigurationManager();
            var service = new ElectronicLibraryRepository(configurationManager.ConnectionString);
        }
    }
}
