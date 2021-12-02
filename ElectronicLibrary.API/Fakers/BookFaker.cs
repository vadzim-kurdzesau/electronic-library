using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.API.Fakers
{
    public static class BookFaker
    {
        private static readonly Faker<Book> Faker;

        static BookFaker()
        {
            Faker = new Faker<Book>()
                .RuleFor(b => b.Id, f => f.IndexFaker)
                .RuleFor(b => b.Name, f => f.Lorem.Word())
                .RuleFor(b => b.Author, f => f.Name.FullName())
                .RuleFor(b => b.PublicationDate, f => f.Date.Past());
        }

        internal static void SeedBooksTable(ElectronicLibraryService service)
        {
            var books = Faker.Generate(100);
            foreach (var book in books)
            {
                service.InsertBook(book);
            }
        }
    }
}
