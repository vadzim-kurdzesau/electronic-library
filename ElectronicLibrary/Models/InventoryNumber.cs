using Dapper.Contrib.Extensions;

namespace ElectronicLibrary.Models
{
    [Table("dbo.inventory_numbers")]
    public class InventoryNumber
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string Number { get; set; }
    }
}
