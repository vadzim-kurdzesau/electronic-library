using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Repositories
{
    internal sealed class InventoryNumbersRepository: BaseRepository
    {
        internal InventoryNumbersRepository(string connectionString) : base(connectionString)
        {
        }

        public IEnumerable<InventoryNumber> Get(Book book)
            => this.Get(book.Id);

        public IEnumerable<InventoryNumber> Get(int bookId)
        {
            using var connection = this.GetSqlConnection();

            const string queryString = "dbo.sp_inventory_numbers_read_by_book_id";
            foreach (var inventoryNumber in this.InitializeAndQueryStoredProcedure(queryString, new { Id = bookId }))
            {
                yield return inventoryNumber;
            }
        }

        public void TakeBook(Book book, Reader reader)
        {
            using var connection = this.GetSqlConnection();
            var inventoryNumber = this.InitializeAndQueryStoredProcedure("dbo.sp_inventory_numbers_read_not_borrowed", new {Id = book.Id})
                                      .FirstOrDefault() 
                                        ?? throw new ArgumentException("There are no copies of this book right now.");

            this.InitializeAndExecuteStoredProcedure("dbo.sp_borrow_history_insert",
                new
                {
                    ReaderId = reader.Id,
                    InventoryNumberId = inventoryNumber.Id,
                    BorrowDate = DateTime.Now
                });
        }

        public void ReturnBook(InventoryNumber inventoryNumber)
        {
            using var connection = this.GetSqlConnection();
            this.InitializeAndExecuteStoredProcedure("dbo.sp_borrow_history_update_by_inventory_number", new DynamicParameters(new
            {
                InventoryNumberId = inventoryNumber.Id,
                ReturnDate = DateTime.Now
            }));
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
