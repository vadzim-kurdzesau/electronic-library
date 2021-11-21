using System;

namespace ElectronicLibrary.Models
{
    public class Book
    {
        // todo: why init?
        public int Id { get; init; }

        public string Name { get; set; }

        public string Author { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}
