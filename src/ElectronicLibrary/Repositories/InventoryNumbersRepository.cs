using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Repositories
{
    internal sealed class InventoryNumbersRepository : BaseRepository
    {
        internal InventoryNumbersRepository(string connectionString) : base(connectionString)
        {
        }

        public IEnumerable<InventoryNumber> Get(Book book)
            => this.Get(book.Id);

        public InventoryNumber Get(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentNullException(nameof(number),
                    "Inventory number can't be null, empty or a whitespace.");
            }

            const string queryString = "dbo.sp_inventory_numbers_read_by_name";
            return this.InitializeAndQueryStoredProcedure(queryString, new {Number = number}).FirstOrDefault();
        }

        public IEnumerable<InventoryNumber> Get(int bookId)
        {
            using var connection = this.GetSqlConnection();

            const string queryString = "dbo.sp_inventory_numbers_read_by_book_id";
            foreach (var inventoryNumber in this.InitializeAndQueryStoredProcedure(queryString, new { Id = bookId }))
            {
                yield return inventoryNumber;
            }
        }
        public IEnumerable<InventoryNumber> GetNotBorrowed(Book book)
        {
            using var connection = this.GetSqlConnection();

            const string queryString = "dbo.sp_inventory_numbers_read_not_borrowed";
            return this.InitializeAndQueryStoredProcedure(queryString, new { Id = book.Id });
        }

        private IEnumerable<InventoryNumber> InitializeAndQueryStoredProcedure(string procedureName, object procedureParameters)
        {
            using var connection = this.GetSqlConnection();
            return connection.Query<InventoryNumber>(procedureName, procedureParameters, commandType: CommandType.StoredProcedure);
        }

        public void Insert(InventoryNumber inventoryNumber)
        {
            const string queryString = "dbo.sp_inventory_numbers_insert";
            this.InitializeAndExecuteStoredProcedure(queryString, ProvideInventoryNumberParameters(inventoryNumber));
        }

        private static DynamicParameters ProvideInventoryNumberParameters(InventoryNumber inventoryNumber)
            => new DynamicParameters(new
            {
                BookId = inventoryNumber.BookId,
                Number = inventoryNumber.Number
            });
    }
}
