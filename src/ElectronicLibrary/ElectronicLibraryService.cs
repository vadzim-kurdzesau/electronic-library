using System;
using System.Collections.Generic;
using System.Linq;
using ElectronicLibrary.EntityMaps;
using ElectronicLibrary.Exceptions;
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
        private readonly BorrowHistoryRepository _borrowHistoryRepository;

        static ElectronicLibraryService()
        {
            var fluentMapInitializer = new FluentMapInitializer();
        }

        public ElectronicLibraryService(string connectionString)
        {
            _readersRepository = new ReadersRepository(connectionString);
            _citiesRepository = new CitiesRepository(connectionString);
            _booksRepository = new BooksRepository(connectionString);
            _inventoryNumbersRepository = new InventoryNumbersRepository(connectionString);
            _borrowHistoryRepository = new BorrowHistoryRepository(connectionString);
        }

        public IEnumerable<City> GetAllCities()
            => _citiesRepository.Cities;

        public IEnumerable<Reader> GetAllReaders()
            => _readersRepository.GetAll();

        public IEnumerable<Reader> GetAllReaders(int page, int size)
            => _readersRepository.GetAll(page, size);

        public Reader GetReader(int id)
            => _readersRepository.Get(id);

        public IEnumerable<Reader> GetReaderByName(string firstName, string lastName)
            => _readersRepository.GetByName(firstName, lastName);

        public Reader GetReaderByPhone(string phone)
            => _readersRepository.GetByPhone(phone);

        public Reader GetReaderByEmail(string email)
            => _readersRepository.GetByEmail(email);

        public void InsertReader(Reader reader)
            => _readersRepository.Insert(reader);

        public void UpdateReader(Reader reader)
        {
            if (GetReader(reader.Id) is null)
            {
                throw new ElementNotFoundException("reader", reader.Id);
            }

            _readersRepository.Update(reader);
        }

        public void DeleteReader(int id)
        {
            if (GetReader(id) is null)
            {
                throw new ElementNotFoundException("reader", id);
            }

            _readersRepository.Delete(id);
        }

        public IEnumerable<Book> GetAllBooks()
            => _booksRepository.GetAll();

        public IEnumerable<Book> GetAllBooks(int page, int size)
            => _booksRepository.GetAll(page, size);

        public Book GetBook(int id)
            => _booksRepository.Get(id);

        public void InsertBook(Book book)
            => _booksRepository.Insert(book);

        public IEnumerable<Book> GetBooksByName(string name, int page = 1, int size = 1)
            => _booksRepository.GetBooksBy(page, size, "name", name);

        public IEnumerable<Book> GetBooksByAuthor(string author, int page = 1, int size = 1)
            => _booksRepository.GetBooksBy(page, size, "author", author);

        public IEnumerable<Book> GetAllBooksByAuthorAndName(string author, string name, int page = 1, int size = 1)
            => _booksRepository.GetBooksBy(page, size, ("author", author), ("name", name));

        public bool DeleteBook(int id)
            => _booksRepository.Delete(id);

        public void UpdateBook(Book book)
        {
            if (GetBook(book.Id) is null)
            {
                throw new ElementNotFoundException(nameof(book), book.Id);
            }

            _booksRepository.Update(book);
        }

        public InventoryNumber GetInventoryNumber(string number)
            => _inventoryNumbersRepository.Get(number);

        public IEnumerable<InventoryNumber> GetInventoryNumbers(Book book)
            => _inventoryNumbersRepository.Get(book.Id);

        public IEnumerable<InventoryNumber> GetInventoryNumbers(int bookId)
        {
            if (GetBook(bookId) is null)
            {
                throw new ElementNotFoundException("book", bookId);
            }

            return _inventoryNumbersRepository.Get(bookId);
        } 

        public void InsertInventoryNumber(InventoryNumber inventoryNumber)
            => _inventoryNumbersRepository.Insert(inventoryNumber);

        public InventoryNumber TakeBook(Book book, Reader reader)
        {
            var inventoryNumber = _inventoryNumbersRepository.GetNotBorrowed(book).FirstOrDefault()
                ?? throw new ArgumentException("There are no copies of this book right now.");

            _borrowHistoryRepository.Insert(reader, inventoryNumber);
            return inventoryNumber;
        }

        public void ReturnBook(Reader reader, InventoryNumber inventoryNumber)
            => _borrowHistoryRepository.Update(reader, inventoryNumber);
    }
}
