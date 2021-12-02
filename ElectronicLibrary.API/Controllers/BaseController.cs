using Microsoft.AspNetCore.Mvc;

namespace ElectronicLibrary.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController : Controller
    {
        protected readonly ElectronicLibraryService _electronicLibraryService;

        protected BaseController(ElectronicLibraryService electronicLibraryService)
        {
            _electronicLibraryService = electronicLibraryService;
        }
    }
}
