using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicLibrary.API.Parameters;
using ElectronicLibrary.Exceptions;
using ElectronicLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicLibrary.API.Controllers
{
    public class ReadersController : BaseController
    {
        public ReadersController(ElectronicLibraryService electronicLibraryService) : base(electronicLibraryService)
        {
        }

        [HttpGet]
        public IActionResult GetAllReaders([FromQuery] PaginationParameters paginationParameters)
        {
            var readers = this._electronicLibraryService.GetAllReaders(paginationParameters.Page, paginationParameters.Size);

            return Ok(readers);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetReader(int id)
        {
            var reader = this._electronicLibraryService.GetReader(id);

            if (reader is null)
            {
                return NotFound();
            }

            return Ok(reader);
        }

        [HttpPost]
        public IActionResult InsertReader(Reader reader)
        {
            if (reader is null)
            {
                return BadRequest();
            }

            this._electronicLibraryService.InsertReader(reader);

            return CreatedAtAction("GetReader", new { id = reader.Id }, reader);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteReader(int id)
        {
            this._electronicLibraryService.DeleteReader(id);

            return Ok();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateReader(int id, Reader reader)
        {
            if (id != reader.Id)
            {
                return BadRequest();
            }

            try
            {
                this._electronicLibraryService.UpdateReader(reader);
            }
            catch (ElementNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("{number:alpha}")]
        public IActionResult ReturnBook([FromBody] int readerId, [FromRoute] string number)
        {
            var reader = this._electronicLibraryService.GetReader(readerId);
            if (reader is null)
            {
                return NotFound(readerId);
            }

            this._electronicLibraryService.ReturnBook(reader, this._electronicLibraryService.GetInventoryNumber(number));

            return Ok(number);
        }

        [HttpPost("{id:int}")]
        public IActionResult TakeBook([FromBody] int bookId, [FromRoute] int id)
        {
            // TODO: add isBorrowed property to inventory_numbers

            if (bookId <= 0 || id <= 0)
            {
                return BadRequest();
            }

            var book = this._electronicLibraryService.GetBook(bookId);
            if (book is null)
            {
                return NotFound(bookId);
            }

            var reader = this._electronicLibraryService.GetReader(id);
            if (reader is null)
            {
                return NotFound(id);
            }

            var inventoryNumber = this._electronicLibraryService.TakeBook(book, reader);

            return Ok(inventoryNumber);
        }
    }
}
