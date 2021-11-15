using System;
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
            this.sqlConnection = new SqlConnection(BuildConnectionString(connectionString));
            this.ReaderRepository = new ReaderRepository(sqlConnection);
            this.sqlConnection.Open();
        }

        public ReaderRepository ReaderRepository { get; }
        
        private static string BuildConnectionString(string connectionString)
        {
            return new SqlConnectionStringBuilder(connectionString)
            {
                ["Integrated Security"] = true,
                ["Initial Catalog"] = "ElectronicLibrary",
                ["Connect Timeout"] = 0,
                ["ApplicationIntent"] = "ReadWrite"
            }.ConnectionString;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
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
