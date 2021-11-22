using System;
using System.Data.SqlClient;

namespace ElectronicLibrary
{
    public class ElectronicLibraryService : IDisposable
    {
        private bool disposedValue;

        public ElectronicLibraryService(string connectionString)
        {
            ValidateConnectionString(connectionString);

            this.ReaderRepository = new ReadersRepository(connectionString);
            //todo: rework and re-enable
            //this.BooksRepository = new BooksRepository(connectionString);
        }

        public ReadersRepository ReaderRepository { get; }

        public BooksRepository BooksRepository { get; }

        public void Dispose()
        {
            //todo: rework and re-enable
            // this.BooksRepository.SaveChanges(this.sqlConnection);
            Dispose(disposing: true);
        }

        // todo: what is missing in Dispose pattern?
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // todo: rework and re-enable, probably for the whole BooksRepository level
                    // this.sqlConnection.Dispose();
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
