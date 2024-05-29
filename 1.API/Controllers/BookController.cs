using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1.API.Request;
using _2.Domain;
using _3.Data;
using _3.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookData _bookData;

        private readonly IBookDomain _bookDomain;

        private readonly IMapper _mapper;
        
        public BookController(IBookData bookData, IBookDomain bookDomain, IMapper mapper)
        {
            _bookData = bookData;
            _bookDomain = bookDomain;
            _mapper = mapper;
        }
        
        // GET: api/Book
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _bookData.GetAllAsync();
            var result = _mapper.Map<List<Book>, List<Book>>(data);
            return Ok(result);
        }
        
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> GetSearchAsync(string? title, string? author, int? year)
        {
            var data = await _bookData.getSearchedAsync(title, author, year);
            var result = _mapper.Map<List<Book>, List<Book>>(data);
            if (result.Count() == 0) return NotFound();
            return Ok(result);
        }
        // GET: api/Book/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _bookData.GetByIdAsync(id);
            var result = _mapper.Map<Book, Book>(data);
            
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/Book
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookRequest data)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                var book = _mapper.Map<BookRequest, Book>(data);
                var result = await _bookDomain.SaveAsync(book);
                return Created("api/book", result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT: api/Book/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BookRequest data)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                var book = _mapper.Map<BookRequest, Book>(data);
                var result = await _bookDomain.UpdateAsync(book, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bookDomain.DeleteAsync(id);
            return Ok(result);
        }
    }
}
