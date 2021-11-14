using System;
using System.Data.SqlClient;

namespace ElectronicLibrary
{
    public class ElectronicLibraryService : IDisposable
    {
        private readonly string connectionString;
        private readonly SqlConnection sqlConnection;
        private bool disposedValue;

        public ElectronicLibraryService(string connectionString)
        {
            ValidateConnectionString(connectionString);
            this.connectionString = connectionString;
            this.sqlConnection = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            this.sqlConnection.Open();
        }

        private static void ValidateConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string can't be null, empty or a whitespace.");
            }
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

        public void Dispose()
        {
            Dispose(disposing: true);
        }
    }
}
