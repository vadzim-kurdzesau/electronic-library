using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicLibrary.API
{
    public class DBConfiguration
    {
        public static void Seed(ConfigurationManager configurationManager)
        {
            using var connection = new SqlConnection(configurationManager.ConnectionString);


        }
    }
}
