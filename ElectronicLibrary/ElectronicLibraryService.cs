using ElectronicLibrary.EntityMaps;
using ElectronicLibrary.Repositories;

namespace ElectronicLibrary
{
    public class ElectronicLibraryService
    {
        public ElectronicLibraryService(string connectionString)
        {
            this.ReaderRepository = new ReadersRepository(connectionString);
            this.CitiesRepository = new CitiesRepository(connectionString);
            this.BooksRepository = new BooksRepository(connectionString);
            this.InventoryNumbersRepository = new InventoryNumbersRepository(connectionString);
            var fluentMapInitializer = new FluentMapInitializer();
        }

        public ReadersRepository ReaderRepository { get; }

        public BooksRepository BooksRepository { get; }

        public CitiesRepository CitiesRepository { get; }

        public InventoryNumbersRepository InventoryNumbersRepository { get; }
    }
}
