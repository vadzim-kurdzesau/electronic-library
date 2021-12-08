using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicLibrary.API.Controllers
{
    public class CitiesController : BaseController
    {
        public CitiesController(ElectronicLibraryService electronicLibraryService) : base(electronicLibraryService)
        {
        }

        [HttpGet]
        public IActionResult GetAllCities()
        {
            var cities = this._electronicLibraryService.GetAllCities;
            return Ok(cities);
        }
    }
}
