using System;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;
using Key = Dapper.Contrib.Extensions.KeyAttribute;

namespace ElectronicLibrary.Models
{
    [Table("dbo.books")]
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Author { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}
