using System.Collections.Generic;
using System.Linq;
using ElectronicLibrary.API.Extensions;
using ElectronicLibrary.API.Parameters;
using ElectronicLibrary.API.ViewModels;
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

            return View("GetAll", this.ConvertToViewModel(readers));
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

        [HttpGet("insert")]
        public IActionResult PostView()
        {
            ViewBag.Cities = this._electronicLibraryService.GetAllCities.ConvertToSelectListItems();
            return View("Insert");
        }

        [HttpPost]
        public IActionResult InsertReader(ReaderViewModel readerViewModel)
        {
            if (readerViewModel is null)
            {
                return BadRequest();
            }

            this._electronicLibraryService.InsertReader(this.ConvertToModel(readerViewModel));

            return Ok();
            //return CreatedAtAction("GetReader", new { id = reader.Id }, reader);
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

        private IEnumerable<ReaderViewModel> ConvertToViewModel(IEnumerable<Reader> readers)
            => readers.Select(this.ConvertToViewModel);

        private Reader ConvertToModel(ReaderViewModel reader)
            => new Reader()
            {
                Id = reader.Id,
                FirstName = reader.FirstName,
                LastName = reader.LastName,
                Email = reader.Email,
                Phone = reader.Phone,
                CityId = this._electronicLibraryService.GetAllCities.First(c => c.Name == reader.City).Id,
                Address = reader.Address,
                Zip = reader.Zip
            };

        private ReaderViewModel ConvertToViewModel(Reader reader)
            => new ReaderViewModel()
            {
                Id = reader.Id,
                FirstName = reader.FirstName,
                LastName = reader.LastName,
                Email = reader.Email,
                Phone = reader.Phone,
                City = this._electronicLibraryService.GetAllCities.First(c => c.Id == reader.CityId).Name,
                Address = reader.Address,
                Zip = reader.Zip
            };
    }
}
