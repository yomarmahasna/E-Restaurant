using E_Restaurant.DTOs.CartDTO.Response;
using E_Restaurant.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace E_Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IMenuService _menuService;

        public CartController(ICartService cartService, IMenuService menuService)
        {
            _cartService = cartService;
            _menuService = menuService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddToCart(int customerId, int menuItemId, int quantity)
        {
            Log.Information("Operation of Add To Cart Has Been Started");
            try
            {
                await _cartService.AddToCartAsync(customerId, menuItemId, quantity);
                Log.Information("Item added to cart successfully");
                return Ok("Item added to cart");
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Adding Item to Cart");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<CartDTO>> GetCart(int customerId)
        {
            Log.Information("Operation of Get Cart Has Been Started");
            try
            {
                var cart = await _cartService.GetCartByUserIdAsync(customerId);
                if (cart == null)
                {
                    Log.Warning($"Cart not found for customer ID {customerId}");
                    return NotFound("Cart not found");
                }
                Log.Information("Cart Retrieved Successfully");
                return Ok(cart);
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Getting Cart");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> ClearCart(int customerId)
        {
            Log.Information("Operation of Clear Cart Has Been Started");
            try
            {
                await _cartService.ClearCartAsync(customerId);
                Log.Information("Cart cleared successfully");
                return Ok("Cart cleared");
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Clearing Cart");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> RemoveItemFromCart(int customerId, int menuItemId)
        {
            Log.Information("Operation of Remove Item From Cart Has Been Started");
            try
            {
                await _cartService.RemoveItemFromCartAsync(customerId, menuItemId);
                Log.Information("Item removed from cart successfully");
                return Ok("Item removed from cart successfully.");
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Removing Item From Cart");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
