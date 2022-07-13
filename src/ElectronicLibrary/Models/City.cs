using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;
using Key = Dapper.Contrib.Extensions.KeyAttribute;

namespace ElectronicLibrary.Models
{
    [Table("dbo.cities")]
    public class City
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }
    }
}
