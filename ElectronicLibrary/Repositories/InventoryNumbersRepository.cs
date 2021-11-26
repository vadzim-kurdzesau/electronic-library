using System.Collections.Generic;
using System.Data;
using Dapper;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Repositories
{
    public class InventoryNumbersRepository: BaseRepository
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

        private static object ProvideInventoryNumberParameters(InventoryNumber inventoryNumber)
            => new
            {
                BookId = inventoryNumber.BookId,
                Number = inventoryNumber.Number
            };
    }
}
