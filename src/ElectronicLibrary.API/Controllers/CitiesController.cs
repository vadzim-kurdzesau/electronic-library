using Microsoft.AspNetCore.Mvc;

namespace ElectronicLibrary.API.Controllers
{
    public class CitiesController : BaseController
    {
        public CitiesController(ElectronicLibraryService electronicLibraryService)
            : base(electronicLibraryService)
        {
            ViewBag.Cities = _electronicLibraryService.GetAllCities();
        }

        [HttpGet]
        public IActionResult GetAllCities()
        {
            var cities = _electronicLibraryService.GetAllCities();
            return Ok(cities);
        }
    }
}
