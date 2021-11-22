using System;
using ElectronicLibrary.Repositories;

namespace ElectronicLibrary
{
    public class ElectronicLibraryService: IDisposable
    {
        private bool _disposedValue;

        public ElectronicLibraryService(string connectionString)
        {
            this.ReaderRepository = new ReadersRepository(connectionString);
            this.CitiesRepository = new CitiesRepository(connectionString);
            this.BooksRepository = new BooksRepository(connectionString);
        }

        public ReadersRepository ReaderRepository { get; }

        public BooksRepository BooksRepository { get; }

        public CitiesRepository CitiesRepository { get; }

        public void Dispose()
        {
            //todo: rework and re-enable
            // this.BooksRepository.SaveChanges(this.sqlConnection);
            Dispose(disposing: true);
        }

        // todo: what is missing in Dispose pattern?
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // todo: rework and re-enable, probably for the whole BooksRepository level
                    // this.sqlConnection.Dispose();
                }

                _disposedValue = true;
            }
        }
    }
}
