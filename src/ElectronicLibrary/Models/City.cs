using Dapper.Contrib.Extensions;

namespace ElectronicLibrary.Models
{
    [Table("dbo.cities")]
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
