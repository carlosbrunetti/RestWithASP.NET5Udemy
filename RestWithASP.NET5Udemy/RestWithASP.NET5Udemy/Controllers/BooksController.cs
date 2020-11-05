using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASP.NET5Udemy.Business;
using RestWithASP.NET5Udemy.Data.VO;
using RestWithASP.NET5Udemy.Hypermedia.Filters;
using RestWithASP.NET5Udemy.Model;

namespace RestWithASP.NET5Udemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
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
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_booksBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var book = _booksBusiness.FindByID(id);
            if (book == null)
                return NotFound();
            return Ok(_booksBusiness.FindByID(id));
        }
        
        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] BookVO books)
        {
            if (books == null)
                return BadRequest();
            return Ok(_booksBusiness.Create(books));
        }

        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] BookVO books)
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