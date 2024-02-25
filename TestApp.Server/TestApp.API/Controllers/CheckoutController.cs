using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

using TestApp.Services.Contracts;
using TestApp.Services.Contracts.Business;
using TestApp.Services.Exceptions;

namespace TestApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class CheckoutController : Controller
    {
        public ICheckoutService _checkoutService { get; }
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController(IMapper mapper, ILogger<CheckoutController> logger,
            ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
            _mapper = mapper;
            _logger = logger;
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
        [HttpGet]
        public async Task<IActionResult> Price(string userId)
        {
            try
            {
                var price = await _checkoutService.PriceAsync(userId);

                return Ok(price);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error Fetching Price");
                return StatusCode(500, "Error Fetching Price");
            }

        }
    }
}
