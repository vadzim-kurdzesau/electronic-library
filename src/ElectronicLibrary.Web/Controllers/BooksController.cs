using System.Linq;
using ElectronicLibrary.Exceptions;
using ElectronicLibrary.Web.Parameters;
using ElectronicLibrary.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicLibrary.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly ElectronicLibraryService _electronicLibraryService;

        public BooksController(ElectronicLibraryService electronicLibraryService)
        {
            _electronicLibraryService = electronicLibraryService;
        }

        [HttpGet]
        public ActionResult Index([FromQuery] BookParameters bookParameters)
        {
            if (bookParameters.Title is null)
            {
                if (bookParameters.Author is null)
                {
                    return View(_electronicLibraryService.GetAllBooks(bookParameters.Page, bookParameters.Size)
                        .Select(b => BookViewModel.ToViewModel(b)));
                }

                return View(_electronicLibraryService.GetBooksByName(bookParameters.Title, bookParameters.Page, bookParameters.Size)
                    .Select(b => BookViewModel.ToViewModel(b)));
            }

            return View(_electronicLibraryService.GetAllBooksByAuthorAndName(bookParameters.Author, bookParameters.Title, bookParameters.Page, bookParameters.Size)
                .Select(b => BookViewModel.ToViewModel(b)));
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var book = _electronicLibraryService.GetBook(id);
            if (book is null)
            {
                return NotFound();
            }

            return View(BookViewModel.ToViewModel(book));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookViewModel book)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _electronicLibraryService.InsertBook(book.ToModel());
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
            var book = _electronicLibraryService.GetBook(id);
            if (book is null)
            {
                return NotFound();
            }

            return View(BookViewModel.ToViewModel(book));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookViewModel book)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _electronicLibraryService.UpdateBook(book.ToModel());
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
            if (!_electronicLibraryService.DeleteBook(id))
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
