using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ElectronicLibrary.API.Parameters;
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
        // TODO: manage to get all books by author/name with pagination parameters
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

            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBook(int id)
        {
            var book = this._electronicLibraryService.GetBook(id);

            if (book is null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult InsertBook(Book book)
        {
            if (book is null)
            {
                return BadRequest();
            }

            this._electronicLibraryService.InsertBook(book);

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
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

        [HttpPut("{id:int}")]
        public IActionResult UpdateBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            try
            {
                this._electronicLibraryService.UpdateBook(book);
            }
            catch (ElementNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
