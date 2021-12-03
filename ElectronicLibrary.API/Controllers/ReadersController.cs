using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicLibrary.API.Parameters;
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
            var readers = this._electronicLibraryService.GetAllReaders()
                                           .Skip(paginationParameters.Size * (paginationParameters.Page - 1))
                                           .Take(paginationParameters.Size);

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

            return CreatedAtAction("GetReader", new {id = reader.Id}, reader);
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
            catch (Exception)
            {
                if (this._electronicLibraryService.GetReader(id) is null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }
    }
}
