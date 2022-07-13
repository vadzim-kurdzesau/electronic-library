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
        public ReadersController(ElectronicLibraryService electronicLibraryService)
            : base(electronicLibraryService)
        {
        }

        [HttpGet]
        public IActionResult GetAllReaders([FromQuery] PaginationParameters paginationParameters)
        {
            var readers = _electronicLibraryService.GetAllReaders(paginationParameters.Page, paginationParameters.Size);
            return View("GetAll", ConvertToViewModel(readers));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetReader(int id)
        {
            var reader = _electronicLibraryService.GetReader(id);
            if (reader is null)
            {
                return NotFound();
            }

            return Ok(reader);
        }

        [HttpGet("insert")]
        public IActionResult PostView()
        {
            ViewBag.Cities = _electronicLibraryService.GetAllCities().ConvertToSelectListItems();
            return View("Insert");
        }

        [HttpPost]
        public IActionResult InsertReader(ReaderViewModel readerViewModel)
        {
            if (readerViewModel is null)
            {
                return BadRequest();
            }

            _electronicLibraryService.InsertReader(ConvertToModel(readerViewModel));
            return Ok();
            //return CreatedAtAction("GetReader", new { id = reader.Id }, reader);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteReader(int id)
        {
            _electronicLibraryService.DeleteReader(id);
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
                _electronicLibraryService.UpdateReader(reader);
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
            var reader = _electronicLibraryService.GetReader(readerId);
            if (reader is null)
            {
                return NotFound(readerId);
            }

            _electronicLibraryService.ReturnBook(reader, _electronicLibraryService.GetInventoryNumber(number));
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

            var book = _electronicLibraryService.GetBook(bookId);
            if (book is null)
            {
                return NotFound(bookId);
            }

            var reader = _electronicLibraryService.GetReader(id);
            if (reader is null)
            {
                return NotFound(id);
            }

            var inventoryNumber = _electronicLibraryService.TakeBook(book, reader);

            return Ok(inventoryNumber);
        }

        private IEnumerable<ReaderViewModel> ConvertToViewModel(IEnumerable<Reader> readers)
            => readers.Select(ConvertToViewModel);

        private Reader ConvertToModel(ReaderViewModel reader)
            => new Reader()
            {
                Id = reader.Id,
                FirstName = reader.FirstName,
                LastName = reader.LastName,
                Email = reader.Email,
                Phone = reader.Phone,
                CityId = _electronicLibraryService.GetAllCities().First(c => c.Name == reader.City).Id,
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
                City = _electronicLibraryService.GetAllCities().First(c => c.Id == reader.CityId).Name,
                Address = reader.Address,
                Zip = reader.Zip
            };
    }
}
