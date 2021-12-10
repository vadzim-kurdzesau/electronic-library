using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
