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

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ElectronicLibraryService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
