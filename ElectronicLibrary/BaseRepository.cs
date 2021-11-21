using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicLibrary
{
    public class BaseRepository
    {
        private readonly string _connectionString;

        internal BaseRepository(string connectionString)
        {
            _connectionString = connectionString;

        }

        internal SqlConnection GetSqlConnection()
        {
            var sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            return sqlConnection;
        }
    }
}
