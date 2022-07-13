using System;
using Dapper;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Repositories
{
    internal sealed class BorrowHistoryRepository : BaseRepository
    {
        internal BorrowHistoryRepository(string connectionString)
            : base(connectionString)
        {
        }

        public void Insert(Reader reader, InventoryNumber inventoryNumber)
        {
            using var connection = GetSqlConnection();

            const string procedureName = "dbo.sp_borrow_history_insert";
            InitializeAndExecuteStoredProcedure(procedureName, new DynamicParameters(new
                {
                    ReaderId = reader.Id,
                    InventoryNumberId = inventoryNumber.Id,
                    BorrowDate = DateTime.Now
                }));
        }

        public void Update(Reader reader, InventoryNumber inventoryNumber)
            => Update(ConvertToHistoryRecord(reader, inventoryNumber));

        public void Update(BorrowHistoryRecord record)
        {
            using var connection = GetSqlConnection();
            InitializeAndExecuteStoredProcedure("dbo.sp_borrow_history_update", new DynamicParameters(new
            {
                ReaderId = record.ReaderId,
                InventoryNumberId = record.InventoryNumberId,
                ReturnDate = DateTime.Now
            }));
        }

        private static BorrowHistoryRecord ConvertToHistoryRecord(Reader reader, InventoryNumber inventoryNumber)
            => new BorrowHistoryRecord()
            {
                ReaderId = reader.Id,
                InventoryNumberId = inventoryNumber.Id
            };
    }
}
