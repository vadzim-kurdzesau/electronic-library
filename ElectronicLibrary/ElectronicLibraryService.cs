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
            this._readersRepository = new ReadersRepository(connectionString);
            this._citiesRepository = new CitiesRepository(connectionString);
            this._booksRepository = new BooksRepository(connectionString);
            this._inventoryNumbersRepository = new InventoryNumbersRepository(connectionString);
            this._borrowHistoryRepository = new BorrowHistoryRepository(connectionString);
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
        {
            if (this.GetReader(reader.Id) is null)
            {
                throw new ElementNotFoundException(nameof(reader), reader.Id);
            }

            this._readersRepository.Update(reader);
        }

        public void DeleteReader(int id)
        {
            if (this.GetReader(id) is null)
            {
                throw new ElementNotFoundException("reader", id);
            }

            this._readersRepository.Delete(id);
        }

        public IEnumerable<Book> GetAllBooks()
            => this._booksRepository.GetAll();

        public Book GetBook(int id)
            => this._booksRepository.Get(id);

        public void InsertBook(Book book)
            => this._booksRepository.Insert(book);

        public IEnumerable<Book> GetBooksByName(string name)
            => this._booksRepository.GetByName(name);

        public IEnumerable<Book> GetBooksByAuthor(string author)
            => this._booksRepository.GetByAuthor(author);

        public bool DeleteBook(int id)
        {
            if (this.GetBook(id) is null)
            {
                throw new ElementNotFoundException("book", id);
            }

            return this._booksRepository.Delete(id);
        }

        public void UpdateBook(Book book)
        {
            if (this.GetBook(book.Id) is null)
            {
                throw new ElementNotFoundException(nameof(book), book.Id);
            }

            this._booksRepository.Update(book);
        }

        public IEnumerable<InventoryNumber> GetInventoryNumbers(Book book)
            => this._inventoryNumbersRepository.Get(book.Id);

        public IEnumerable<InventoryNumber> GetInventoryNumbers(int bookId)
        {
            if (this.GetBook(bookId) is null)
            {
                throw new ElementNotFoundException("book", bookId);
            }

            return this._inventoryNumbersRepository.Get(bookId);
        } 

        public void InsertInventoryNumber(InventoryNumber inventoryNumber)
            => this._inventoryNumbersRepository.Insert(inventoryNumber);

        public InventoryNumber TakeBook(Book book, Reader reader)
        {
            var inventoryNumber = this._inventoryNumbersRepository.GetNotBorrowed(book)
                                      .FirstOrDefault()
                                  ?? throw new ArgumentException("There are no copies of this book right now.");

            this._borrowHistoryRepository.Insert(reader, inventoryNumber);
            return inventoryNumber;
        }

        public void ReturnBook(Reader reader, InventoryNumber inventoryNumber)
            => this._borrowHistoryRepository.Update(reader, inventoryNumber);
    }
}
