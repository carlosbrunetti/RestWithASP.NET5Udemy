using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASP.NET5Udemy.Model;
using RestWithASP.NET5Udemy.Business;
using RestWithASP.NET5Udemy.Data.VO;
using RestWithASP.NET5Udemy.Hypermedia.Filters;
using Microsoft.AspNetCore.Authorization;

namespace RestWithASP.NET5Udemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{Version:apiVersion}")]
    public class PersonController : ControllerBase
    {

        private IPersonBusiness _personBusiness;
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get([FromQuery] string name,string sortDirection,int pageSize,int page)
        {
            return Ok(_personBusiness.FindWithPagedSearch(name,sortDirection,pageSize,page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindByID(id);
            if (person == null) return NotFound();
            return Ok(_personBusiness.FindByID(id));
        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Create(person));
        }

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Update(person));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((204), Type = typeof(PersonVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Patch(long id)
        {
            return  Ok(_personBusiness.Disabled(id));
        }

        [HttpGet("findPersonByName")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get([FromQuery] string firstName,[FromQuery] string secondName)
        {
            var person = _personBusiness.FindByName(firstName,secondName);
            if (person == null) return NotFound();
            return Ok(person);
        }

    }
}
