using System;
using Dapper;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Repositories
{
    internal class BorrowHistoryRepository : BaseRepository
    {
        internal BorrowHistoryRepository(string connectionString) : base(connectionString)
        {
        }

        public void Insert(Reader reader, InventoryNumber inventoryNumber)
        {
            using var connection = this.GetSqlConnection();

            const string queryString = "dbo.sp_borrow_history_insert";
            this.InitializeAndExecuteStoredProcedure(queryString, new DynamicParameters(new
                {
                    ReaderId = reader.Id,
                    InventoryNumberId = inventoryNumber.Id,
                    BorrowDate = DateTime.Now
                }));
        }

        public void Update(Reader reader, InventoryNumber inventoryNumber)
            => this.Update(ConvertToHistoryRecord(reader, inventoryNumber));

        private BorrowHistoryRecord ConvertToHistoryRecord(Reader reader, InventoryNumber inventoryNumber)
            => new BorrowHistoryRecord()
            {
                ReaderId = reader.Id,
                InventoryNumberId = inventoryNumber.Id
            };

        public void Update(BorrowHistoryRecord record)
        {
            using var connection = this.GetSqlConnection();
            this.InitializeAndExecuteStoredProcedure("dbo.sp_borrow_history_update", new DynamicParameters(new
            {
                ReaderId = record.ReaderId,
                InventoryNumberId = record.InventoryNumberId,
                ReturnDate = DateTime.Now
            }));
        }
    }
}
