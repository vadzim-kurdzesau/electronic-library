using System;
using System.Data.SqlClient;

namespace ElectronicLibrary.Repositories
{
    public class BaseRepository
    {
        private readonly string _connectionString;

        internal BaseRepository(string connectionString)
        {
            ValidateConnectionString(connectionString);
            _connectionString = connectionString;
        }

        internal SqlConnection GetSqlConnection()
        {
            var sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            return sqlConnection;
        }

        private static void ValidateConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string can't be null, empty or a whitespace.");
            }
        }
    }
}
