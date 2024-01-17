using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using TestApp.API.Model.Validators;
using TestApp.Services.Contracts;
using TestApp.Services.Exceptions;

namespace TestApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class PersonsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PersonsController> _logger;
        private readonly IPersonService _personService;

        public PersonsController(IMapper mapper, ILogger<PersonsController> logger, 
            IPersonService personService)
        {
            _mapper = mapper;
            _logger = logger;
            _personService = personService;
        }



        /// <summary>
        /// Add a new person.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/person
        ///     {
        ///        "firstName": "John",
        ///        "lastName": "Doe",
        ///     }
        ///
        /// </remarks>
        /// <param name="personDto">Person data.</param>
        /// <returns>A newly created person.</returns>
        /// <response code="200">Returns the newly created person.</response>
        /// <response code="400">If the request is invalid.</response>
        /// /// <response code="409">If the person already exists.</response>
        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody] Person person)
        {
            try
            {
                var savedPerson = await _personService.AddPersonAsync(_mapper.Map<IPerson>(person));

                return Ok(_mapper.Map<Person>(savedPerson));
            }
            catch (AlreadyExistsException aex)
            {
                _logger.LogError(aex, aex.Message, person);
                return StatusCode(409, aex.Message);
            }
            catch (Exception e)
            {
               _logger.LogError(e, "Error Saving Person Record", person);
               return StatusCode(500, "Error Saving Person Record");
            }
            
        }
    }
}
