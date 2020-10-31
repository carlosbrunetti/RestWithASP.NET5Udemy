using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASP.NET5Udemy.Business;
using RestWithASP.NET5Udemy.Model;

namespace RestWithASP.NET5Udemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{Version:apiVersion}")]
    public class BooksController : ControllerBase
    {
        private IBooksBusiness _booksBusiness;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBooksBusiness booksBusiness,ILogger<BooksController> logger)
        {
            _booksBusiness = booksBusiness;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_booksBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var book = _booksBusiness.FindByID(id);
            if (book == null)
                return NotFound();
            return Ok(_booksBusiness.FindByID(id));
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] Book books)
        {
            if (books == null)
                return BadRequest();
            return Ok(_booksBusiness.Create(books));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Book books)
        {
            if (books == null)
                return BadRequest();
            return Ok(_booksBusiness.Update(books));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _booksBusiness.Delete(id);
            return NoContent();
        }
        
    }
}