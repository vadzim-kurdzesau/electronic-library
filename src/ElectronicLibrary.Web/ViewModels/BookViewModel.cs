using ElectronicLibrary.Models;
using ElectronicLibrary.Web.Constants;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ElectronicLibrary.Web.ViewModels
{
    /// <summary>
    /// Represents the book view model.
    /// </summary>
    public class BookViewModel
    {
        /// <summary>
        /// Gets or sets the book id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        [Required]
        [StringLength(BookConstants.TitleLength)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the author of the book.
        /// </summary>
        [Required]
        [StringLength(BookConstants.AuthorLength)]
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the publication date of the book.
        /// </summary>
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Date is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Gets or sets the short description of the book.
        /// </summary>
        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }

        /// <summary>
        /// Converts the view model to DBO model.
        /// </summary>
        /// <returns>Converted to the DBO model view model.</returns>
        public Book ToModel()
        {
            return new Book()
            {
                Id = this.Id,
                Name = this.Title,
                Author = this.Author,
                PublicationDate = this.PublicationDate,
            };
        }

        /// <summary>
        /// Converts DBO <paramref name="book"/> to the view model.
        /// </summary>
        /// <param name="book">The DBO book to convert.</param>
        /// <returns>Converted to the view model <paramref name="book"/>.</returns>
        public static BookViewModel ToViewModel(Book book)
        {
            return new BookViewModel()
            {
                Id = book.Id,
                Title = book.Name,
                Author = book.Author,
                PublicationDate = book.PublicationDate,
            };
        }
    }
}
