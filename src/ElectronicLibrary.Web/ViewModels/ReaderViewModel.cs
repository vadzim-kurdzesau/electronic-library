using ElectronicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicLibrary.Web.ViewModels
{
    public class ReaderViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Email { get; set; }

        public string Phone { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Zip { get; set; }

        /// <summary>
        /// Converts the view model to DBO model.
        /// </summary>
        /// <returns>Converted to the DBO model view model.</returns>
        public Reader ToModel(IEnumerable<City> cities)
        {
            return new Reader()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Phone = Phone,
                CityId = cities.First(c => c.Name.Equals(City, StringComparison.CurrentCultureIgnoreCase)).Id,
                Address = Address,
                Zip = Zip,
            };
        }

        /// <summary>
        /// Converts DBO <paramref name="reader"/> to the view model.
        /// </summary>
        /// <param name="reader">The DBO book to convert.</param>
        /// <returns>Converted to the view model <paramref name="reader"/>.</returns>
        public static ReaderViewModel ToViewModel(Reader reader, IEnumerable<City> cities)
        {
            return new ReaderViewModel()
            {
                Id = reader.Id,
                FirstName = reader.FirstName,
                LastName = reader.LastName,
                Email = reader.Email,
                Phone = reader.Phone,
                City = cities.First(c => c.Id == reader.CityId).Name,
                Address = reader.Address,
                Zip = reader.Zip,
            };
        }
    }
}
