using System;
using System.ComponentModel.DataAnnotations;

namespace ElectronicLibrary.API.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You need to enter book's name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You need to enter book's author.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "You need to enter book's publication date.")]
        public DateTime PublicationDate { get; set; }
    }
}
