using System.Collections.Generic;
using System.Data;
using Dapper;
using Dapper.FluentMap;
using ElectronicLibrary.EntityMaps;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Repositories
{
    public class InventoryNumbersRepository: BaseRepository
    {
        static InventoryNumbersRepository()
            => FluentMapper.Initialize(config =>
            {
                config.AddMap(new InventoryNumberMap());
            });

        internal InventoryNumbersRepository(string connectionString) : base(connectionString)
        {
        }

        public IEnumerable<InventoryNumber> GetInventoryNumbers(Book book)
            => this.GetInventoryNumbers(book.Id);

        public IEnumerable<InventoryNumber> GetInventoryNumbers(int bookId)
        {
            using var connection = this.GetSqlConnection();

            const string queryString = @"SELECT *
                                           FROM dbo.inventory_numbers
                                          WHERE dbo.inventory_numbers.book_id = @Id";

            foreach (var inventoryNumber in connection.Query<InventoryNumber>(queryString, new { Id = bookId }))
            {
                yield return inventoryNumber;
            }
        }

        public void InsertInventoryNumber(InventoryNumber inventoryNumber)
        {
            const string queryString = "dbo.sp_inventory_numbers_insert";
            using var connection = this.GetSqlConnection();
            connection.Execute(queryString, ProvideInventoryNumberParameters(inventoryNumber), commandType: CommandType.StoredProcedure);
        }

        private static object ProvideInventoryNumberParameters(InventoryNumber inventoryNumber)
            => new
            {
                BookId = inventoryNumber.BookId,
                Number = inventoryNumber.Number
            };
    }
}
