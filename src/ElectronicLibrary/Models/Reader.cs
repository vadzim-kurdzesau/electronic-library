using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;
using Key = Dapper.Contrib.Extensions.KeyAttribute;

namespace ElectronicLibrary.Models
{
    [Table("dbo.readers")]
    public class Reader
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }

        [MaxLength(320)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        public int CityId { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(6)]
        public string Zip { get; set; }
    }
}
