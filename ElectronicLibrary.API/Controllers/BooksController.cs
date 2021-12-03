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
        public IActionResult GetAllBooks([FromQuery] PaginationParameters paginationParameters)
        {
            var books = this._electronicLibraryService.GetAllBooks(paginationParameters.Page, paginationParameters.Size);

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

        //[HttpGet]
        //public IActionResult GetBooksByAuthor([FromQuery] string author)
        //{
        //    var books = this._electronicLibraryService.GetBooksByAuthor(author);

        //    if (!books.Any())
        //    {
        //        return NotFound();
        //    }

        //    return Ok(books);
        //}

        //[HttpGet]
        //public IActionResult GetBooksByName([FromQuery] string name)
        //{
        //    var books = this._electronicLibraryService.GetBooksByName(name);

        //    if (!books.Any())
        //    {
        //        return NotFound();
        //    }

        //    return Ok(books);
        //}

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
