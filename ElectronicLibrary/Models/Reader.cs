using Dapper.Contrib.Extensions;

namespace ElectronicLibrary.Models
{
    [Table("dbo.readers")]
    public class Reader
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int CityId { get; set; }

        public string Address { get; set; }

        public string Zip { get; set; }
    }
}
