using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ElectronicLibrary.Demo
{
    public class ConfigurationManager
    {
        public ConfigurationManager()
        {
            this.Configuration = new ConfigurationBuilder().SetBasePath(GetPathToConfigurationFile())
                .AddJsonFile("appsettings.json").Build();
            this.ConnectionString = BuildConnectionString();
        } 

        public IConfigurationRoot Configuration { get; }

        public string ConnectionString { get; }

        private string BuildConnectionString()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = this.Configuration["ConnectionStrings:Data Source"],
                InitialCatalog = this.Configuration["ConnectionStrings:Initial Catalog"],
                IntegratedSecurity = bool.Parse(this.Configuration["ConnectionStrings:Integrated Security"]),
                ConnectTimeout = int.Parse(this.Configuration["ConnectionStrings:Connect Timeout"]),
                Encrypt = bool.Parse(this.Configuration["ConnectionStrings:Encrypt"]),
                TrustServerCertificate = bool.Parse(this.Configuration["ConnectionStrings:TrustServerCertificate"]),
                ApplicationIntent = ParseApplicationIntent(this.Configuration["ConnectionStrings:ApplicationIntent"]),
                MultiSubnetFailover = bool.Parse(this.Configuration["ConnectionStrings:MultiSubnetFailover"])
            };

            return connectionStringBuilder.ConnectionString;
        }

        private static ApplicationIntent ParseApplicationIntent(string applicationIntent)
        {
            if (ApplicationIntent.TryParse(applicationIntent, true, out ApplicationIntent result))
            {
                return result;
            }

            throw new ArgumentException("Invalid 'ApplicationIntent' value.");
        }

        private static string GetPathToConfigurationFile()
            => GetPathToHigherDirectory(GetPathToHigherDirectory((Directory.GetCurrentDirectory())));

        private static string GetPathToHigherDirectory(string path)
            => Directory.GetParent(path).Parent.FullName;
    }
}
