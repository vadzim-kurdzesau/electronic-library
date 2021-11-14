using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ElectronicLibrary
{
    public class ElectronicLibraryService : IDisposable
    {
        private readonly SqlConnection sqlConnection;
        private bool disposedValue;

        public ElectronicLibraryService(string connectionString)
        {
            ValidateConnectionString(connectionString);
            this.sqlConnection = new SqlConnection(connectionString);
            this.sqlConnection.Open();
        }

        public void Dispose()
        {
            Dispose(disposing: true);
        }

        public IEnumerable<SqlDataReader> GetAllReaders()
        {
            const string queryString = "SELECT * FROM dbo.readers";
            return this.GetResponseRows(this.InitializeCommand(queryString));
        }

        public IEnumerable<SqlDataReader> GetReaderByName(string firstName, string lastName)
        {
            const string queryString = "GetReaderByName";
            return this.GetResponseRows(ProvideNameParameters(command: this.InitializeCommand(queryString), firstName, lastName));
        }

        private IEnumerable<SqlDataReader> GetResponseRows(SqlCommand command)
        {
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                yield return reader;
            }
        }

        private SqlCommand InitializeCommand(string queryString)
        {
            return new SqlCommand(queryString, this.sqlConnection);
        }

        private static SqlCommand ProvideNameParameters(SqlCommand command, string firstName, string lastName)
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);

            return command;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.sqlConnection.Dispose();
                }

                disposedValue = true;
            }
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
