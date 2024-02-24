using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using TestApp.Services.Contracts;
using TestApp.Services.Contracts.Business;
using TestApp.Services.Exceptions;
using TestApp.Services.Impl.Model;

namespace TestApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class CartController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CartController> _logger;
        private readonly ICartService _cartService;

        public CartController(IMapper mapper, ILogger<CartController> logger, ICartService cartService)
        {
            _mapper = mapper;
            _logger = logger;
            _cartService = cartService;
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
        /// <param name="Item">Cart Item</param>
        /// <response code="200">Returns the newly created person.</response>
        /// <response code="400">If the request is invalid.</response>
        /// /// <response code="409">If the person already exists.</response>
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] Item item)
        {
            try
            {
                await _cartService.Scan(item);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error Saving Item", item);
                return BadRequest();
            }


        }
    }
}
