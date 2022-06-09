using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicLibrary.API.ViewModels
{
    public class ReaderViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You need to enter your first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You need to enter your first name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You need to enter your email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You need to enter your phone.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "You need to enter your city.")]
        public string City { get; set; }

        [Required(ErrorMessage = "You need to enter your address.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "You need to enter your zip code.")]
        public string Zip { get; set; }
    }
}
