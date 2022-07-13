using System.Collections.Generic;
using System.Linq;
using ElectronicLibrary.API.ViewModels;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.API.Extensions
{
    internal static class BookViewModelExtensions
    {
        public static Book ConvertToModel(this BookViewModel bookViewModel)
            => new Book()
            {
                Id = bookViewModel.Id,
                Name = bookViewModel.Name,
                Author = bookViewModel.Author,
                PublicationDate = bookViewModel.PublicationDate
            };

        public static BookViewModel ConvertToViewModel(this Book book)
            => new BookViewModel()
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                PublicationDate = book.PublicationDate
            };

        public static IEnumerable<BookViewModel> ConvertToViewModel(this IEnumerable<Book> books)
            => books.Select(b => b.ConvertToViewModel()).ToList();
    }
}
