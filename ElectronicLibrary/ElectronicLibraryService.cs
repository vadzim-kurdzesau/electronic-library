using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public BooksRepository BooksRepository => this._booksRepository;

        public CitiesRepository CitiesRepository => this._citiesRepository;

        public InventoryNumbersRepository InventoryNumbersRepository => this._inventoryNumbersRepository;
    }
}
