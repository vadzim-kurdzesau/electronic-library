using System;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConfigurationManager configurationManager = new ConfigurationManager();
            using var service = new ElectronicLibraryRepository(configurationManager.ConnectionString);
        }
    }
}
