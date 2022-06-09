using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ElectronicLibrary.API.Extensions;
using ElectronicLibrary.API.Parameters;
using ElectronicLibrary.API.ViewModels;
using ElectronicLibrary.Exceptions;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.API.Controllers
{
    public class BooksController : BaseController
    {
        public BooksController(ElectronicLibraryService electronicLibraryService) : base(electronicLibraryService)
        {
        }

        [HttpGet]
        public IActionResult GetAllBooks([FromQuery] BookParameters bookParameters)
        {
            IEnumerable<Book> books;
            if (bookParameters.Author != null)
            {
                if (bookParameters.Name != null)
                {
                    books = this._electronicLibraryService.GetAllBooksByAuthorAndName(bookParameters.Author,
                        bookParameters.Name, bookParameters.Page, bookParameters.Size);
                }
                else
                {
                    books = this._electronicLibraryService.GetBooksByAuthor(bookParameters.Author, bookParameters.Page, bookParameters.Size);
                }
            }
            else
            {
                books = this._electronicLibraryService.GetAllBooks(bookParameters.Page, bookParameters.Size);
            }

            return View("GetAll", books.ConvertToViewModel());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBook(int id)
        {
            var book = this._electronicLibraryService.GetBook(id);

            if (book is null)
            {
                return NotFound();
            }

            return View("Get", book.ConvertToViewModel());
        }

        [HttpGet("insert")]
        public IActionResult PostView()
        {
            return View("Insert");
        }

        [HttpPost]
        public IActionResult InsertBook(BookViewModel bookViewModel)
        {
            if (ModelState.IsValid)
            {
                this._electronicLibraryService.InsertBook(bookViewModel.ConvertToModel());
            }

            return CreatedAtAction("GetBook", new { id = bookViewModel.Id }, bookViewModel);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteBook(int id)
        {
            if (this._electronicLibraryService.DeleteBook(id))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet("update")]
        public IActionResult Update()
        {
            return View("Update");
        }

        [HttpPut]
        public IActionResult UpdateBook(BookViewModel bookViewModel)
        {
            try
            {
                this._electronicLibraryService.UpdateBook(bookViewModel.ConvertToModel());
            }
            catch (ElementNotFoundException)
            {
                return NotFound();
            }

            return View("Get", bookViewModel);
        }
    }
}