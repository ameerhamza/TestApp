using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using TestApp.Services.Contracts;
using TestApp.Services.Contracts.Business;
using TestApp.Services.Contracts.Common;
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

        public CartController(IMapper mapper, ILogger<CartController> logger,
            ICartService cartService)
        {
            _mapper = mapper;
            _logger = logger;
            _cartService = cartService;
        }

        
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] Item item, string userId)
        {
            try
            {
                await _cartService.Scan(item, userId);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error Saving Item", item);
                return BadRequest();
            }


        }

        [HttpGet]
        public IActionResult GetItems(string userId)
        {
            try
            {
                return Ok(_cartService.GetCartItems(userId));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting cart", userId);
                return BadRequest();
            }


        }

        [HttpDelete]
        public IActionResult ClearItems(string userId)
        {
            try
            {
                _cartService.ClearCart(userId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting cart", userId);
                return BadRequest();
            }


        }
    }
}
