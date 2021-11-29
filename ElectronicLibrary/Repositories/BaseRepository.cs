using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace ElectronicLibrary.Repositories
{
    internal class BaseRepository
    {
        private readonly string _connectionString;

        internal BaseRepository(string connectionString)
        {
            ValidateConnectionString(connectionString);
            this._connectionString = connectionString;
        }

        internal SqlConnection GetSqlConnection()
        {
            var sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            return sqlConnection;
        }

        internal static void ValidateId(int id)
        {
            if (id <= 0)
            {
                throw new ApplicationException("Id can't be negative or equal zero.");
            }
        }

        private static void ValidateConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string can't be null, empty or a whitespace.");
            }
        }

        protected void InitializeAndExecuteStoredProcedure(string procedureName, object procedureParameters)
        {
            using var connection = this.GetSqlConnection();
            connection.Execute(procedureName, procedureParameters, commandType: CommandType.StoredProcedure);
        }
    }
}
