using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicLibrary.API.Fakers;
using ElectronicLibrary.API.Parameters;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.API.Controllers
{
    public class BooksController : BaseController
    {
        public BooksController(ElectronicLibraryService electronicLibraryService) : base(electronicLibraryService)
        {
        }

        [HttpGet]
        public IActionResult GetAllBooks([FromQuery] PaginationParameters paginationParameters)
        {
            var books = this._electronicLibraryService.GetAllBooks()
                                                                      .Skip(paginationParameters.Size * (paginationParameters.Page - 1))
                                                                      .Take(paginationParameters.Size);

            return Ok(books);
        }

        [HttpGet]
        [Route("{id:int}")]
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

            // Do not know how to get url to return 'Created' status
            return Ok(book);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteBook(int id)
        {
            if (this._electronicLibraryService.DeleteBook(id))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPut]
        public IActionResult UpdateBook(Book book)
        {
            this._electronicLibraryService.UpdateBook(book);

            return Ok(book);
        }
    }
}
