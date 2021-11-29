using System.Collections.Generic;
using ElectronicLibrary.EntityMaps;
using ElectronicLibrary.Models;
using ElectronicLibrary.Repositories;

namespace ElectronicLibrary
{
    public class ElectronicLibraryService
    {
        private readonly ReadersRepository _readersRepository;
        private readonly CitiesRepository _citiesRepository;
        private readonly BooksRepository _booksRepository;
        private readonly InventoryNumbersRepository _inventoryNumbersRepository;

        public ElectronicLibraryService(string connectionString)
        {
            this._readersRepository = new ReadersRepository(connectionString);
            this._citiesRepository = new CitiesRepository(connectionString);
            this._booksRepository = new BooksRepository(connectionString);
            this._inventoryNumbersRepository = new InventoryNumbersRepository(connectionString);
            var fluentMapInitializer = new FluentMapInitializer();
        }

        public IEnumerable<City> GetAllCities => this._citiesRepository.Cities;

        public IEnumerable<Reader> GetAllReaders()
            => this._readersRepository.GetAll();

        public Reader GetReader(int id)
            => this._readersRepository.Get(id);

        public IEnumerable<Reader> GetReaderByName(string firstName, string lastName)
            => this._readersRepository.GetByName(firstName, lastName);

        public Reader GetReaderByPhone(string phone)
            => this._readersRepository.GetByPhone(phone);

        public Reader GetReaderByEmail(string email)
            => this._readersRepository.GetByEmail(email);

        public void InsertReader(Reader reader)
            => this._readersRepository.Insert(reader);

        public void UpdateReader(Reader reader)
            => this._readersRepository.Update(reader);

        public void DeleteReader(int id)
            => this._readersRepository.Delete(id);

        public IEnumerable<Book> GetAllBooks()
            => this._booksRepository.GetAll();

        public Book GetBook(int id)
            => this._booksRepository.Get(id);

        public void InsertBook(Book book)
            => this._booksRepository.Insert(book);

        public IEnumerable<Book> GetBookByName(string name)
            => this._booksRepository.GetByName(name);

        public IEnumerable<Book> GetBookByAuthor(string author)
            => this._booksRepository.GetByAuthor(author);

        public void DeleteBook(int id)
            => this._booksRepository.Delete(id);

        public void UpdateBook(Book book)
            => this._booksRepository.Update(book);

        public IEnumerable<InventoryNumber> GetInventoryNumber(Book book)
            => this._inventoryNumbersRepository.Get(book.Id);

        public IEnumerable<InventoryNumber> GetInventoryNumber(int bookId)
            => this._inventoryNumbersRepository.Get(bookId);

        public void InsertInventoryNumber(InventoryNumber inventoryNumber)
            => this._inventoryNumbersRepository.Insert(inventoryNumber);

        public InventoryNumber TakeBook(Book book, Reader reader)
            => this._inventoryNumbersRepository.TakeBook(book, reader);

        public void ReturnBook(InventoryNumber inventoryNumber)
            => this._inventoryNumbersRepository.ReturnBook(inventoryNumber);
    }
}
