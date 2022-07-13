using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Repositories
{
    internal sealed class InventoryNumbersRepository : BaseRepository
    {
        internal InventoryNumbersRepository(string connectionString)
            : base(connectionString)
        {
        }

        public IEnumerable<InventoryNumber> Get(Book book)
            => Get(book.Id);

        public InventoryNumber Get(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentNullException(nameof(number), "Inventory number can't be null, empty or a whitespace.");
            }

            const string procedureName = "dbo.sp_inventory_numbers_read_by_name";
            return InitializeAndQueryStoredProcedure<InventoryNumber>(procedureName, new {Number = number}).FirstOrDefault();
        }

        public IEnumerable<InventoryNumber> Get(int bookId)
        {
            using var connection = GetSqlConnection();

            const string procedureName = "dbo.sp_inventory_numbers_read_by_book_id";
            foreach (var inventoryNumber in InitializeAndQueryStoredProcedure<InventoryNumber>(procedureName, new { Id = bookId }))
            {
                yield return inventoryNumber;
            }
        }

        public IEnumerable<InventoryNumber> GetNotBorrowed(Book book)
        {
            using var connection = GetSqlConnection();

            const string procedureName = "dbo.sp_inventory_numbers_read_not_borrowed";
            return InitializeAndQueryStoredProcedure<InventoryNumber>(procedureName, new { Id = book.Id });
        }

        public void Insert(InventoryNumber inventoryNumber)
        {
            const string procedureName = "dbo.sp_inventory_numbers_insert";
            InitializeAndExecuteStoredProcedure(procedureName, ProvideInventoryNumberParameters(inventoryNumber));
        }

        private static DynamicParameters ProvideInventoryNumberParameters(InventoryNumber inventoryNumber)
            => new DynamicParameters(new
            {
                BookId = inventoryNumber.BookId,
                Number = inventoryNumber.Number
            });
    }
}
