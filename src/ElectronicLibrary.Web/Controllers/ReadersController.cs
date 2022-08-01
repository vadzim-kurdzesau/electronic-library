using System.Collections.Generic;
using System.Linq;
using ElectronicLibrary.Exceptions;
using ElectronicLibrary.Models;
using ElectronicLibrary.Web.Parameters;
using ElectronicLibrary.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicLibrary.Web.Controllers
{
    public class ReadersController : Controller
    {
        private readonly ElectronicLibraryService _electronicLibraryService;
        private readonly IEnumerable<City> _cities;

        public ReadersController(ElectronicLibraryService electronicLibraryService)
        {
            _electronicLibraryService = electronicLibraryService;
            _cities = electronicLibraryService.GetAllCities();
        }

        [HttpGet]
        public ActionResult Index([FromQuery] ReaderParameters readerParameters)
        {
            var readers = _electronicLibraryService.GetAllReaders(readerParameters.Page, readerParameters.Size);
            return View(readers.Select(r => ReaderViewModel.ToViewModel(r, _cities)));
        }

        [HttpGet]
        [Route("[controller]/{id:int}")]
        public ActionResult Details(int id)
        {
            var reader = _electronicLibraryService.GetReader(id);
            if (reader is null)
            {
                return NotFound();
            }

            return View(reader);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Cities = _cities;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReaderViewModel reader)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _electronicLibraryService.InsertReader(reader.ToModel(_cities));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]        
        public ActionResult Edit(int id)
        {
            var reader = _electronicLibraryService.GetReader(id);
            if (reader is null)
            {
                return NotFound();
            }

            ViewBag.Cities = _cities;

            return View(ReaderViewModel.ToViewModel(reader, _cities));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ReaderViewModel reader)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _electronicLibraryService.UpdateReader(reader.ToModel(_cities));
                return RedirectToAction(nameof(Index));
            }
            catch (ElementNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                _electronicLibraryService.DeleteReader(id);
            }
            catch (ElementNotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
