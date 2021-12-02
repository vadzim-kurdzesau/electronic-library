﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
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
            catch (Exception)
            {
                if (this._electronicLibraryService.GetBook(id) is null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }
    }
}
