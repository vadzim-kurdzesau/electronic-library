using System;
using Dapper.Contrib.Extensions;

namespace ElectronicLibrary.Models
{
    [Table("dbo.borrow_history")]
    public class BorrowHistoryRecord
    {
        [Key]
        public int Id { get; set; }

        public int ReaderId { get; set; }

        public int InventoryNumberId { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
