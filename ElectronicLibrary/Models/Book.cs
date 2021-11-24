using System;
using Dapper.Contrib.Extensions;

namespace ElectronicLibrary.Models
{
    [Table("dbo.books")]
    public class Book
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}
