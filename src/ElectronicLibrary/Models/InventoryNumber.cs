using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;
using Key = Dapper.Contrib.Extensions.KeyAttribute;

namespace ElectronicLibrary.Models
{
    [Table("dbo.inventory_numbers")]
    public class InventoryNumber
    {
        [Key]
        public int Id { get; set; }

        public int BookId { get; set; }

        [MaxLength(30)]
        public string Number { get; set; }
    }
}
