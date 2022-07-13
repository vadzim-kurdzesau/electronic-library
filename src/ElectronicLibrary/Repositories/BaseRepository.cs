using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace ElectronicLibrary.Repositories
{
    internal abstract class BaseRepository
    {
        private readonly string _connectionString;

        internal BaseRepository(string connectionString)
        {
            _connectionString = ValidateConnectionString(connectionString);
        }

        internal SqlConnection GetSqlConnection()
        {
            var sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            return sqlConnection;
        }

        internal static int ValidateId(int id)
        {
            if (id <= 0)
            {
                throw new ApplicationException("Id can't be negative or equal zero.");
            }

            return id;
        }

        protected void InitializeAndExecuteStoredProcedure(string procedureName, object procedureParameters)
        {
            using var connection = GetSqlConnection();
            connection.Execute(procedureName, procedureParameters, commandType: CommandType.StoredProcedure);
        }

        protected IEnumerable<T> InitializeAndQueryStoredProcedure<T>(string procedureName, object procedureParameters)
        {
            using var connection = GetSqlConnection();
            return connection.Query<T>(procedureName, procedureParameters, commandType: CommandType.StoredProcedure);
        }

        protected IEnumerable<T> ExecuteQuery<T>(string queryString, DynamicParameters parameters)
        {
            using var connection = GetSqlConnection();
            return connection.Query<T>(queryString, parameters);
        }

        protected static string ValidateString(string stringToValidate)
        {
            if (string.IsNullOrWhiteSpace(stringToValidate))
            {
                throw new ArgumentNullException(nameof(stringToValidate), "String can't be null, empty or a whitespace.");
            }

            return stringToValidate;
        }

        protected static void ValidatePaginationParameters(int page, int size)
        {
            if (page <= 0 || size < 0)
            {
                throw new ArgumentException("Invalid size or page argument.");
            }
        }

        private static string ValidateConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string can't be null, empty or a whitespace.");
            }

            return connectionString;
        }
    }
}
