using E_Restaurant.DTOs.MenuDTO.Request;
using E_Restaurant.DTOs.OrderDTO.Request;
using E_Restaurant.DTOs.ReviewDTO.Request;
using E_Restaurant.DTOs.UserDTO.Request;
using E_Restaurant.DTOs.UserDTO.Response;
using E_Restaurant.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace E_Restaurant.Controllers
{
    public class RestaurantController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IReviewService _reviewService;
        private readonly IUserService _userService;
        private readonly IMenuService _menuService;


        public RestaurantController(IOrderService orderService, IReviewService reviewService, IUserService userService, IMenuService menuService)
        {
            _orderService = orderService;
            _reviewService = reviewService;
            _userService = userService;
            _menuService = menuService;
        }
        // Menu Endpoints
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetMenuItems()
        {
            Log.Information("Operation of Get Menu Items Has Been Started");
            try
            {
                var responses = await _menuService.GetMenuItemsAsync();
                Log.Information($"Menu Items Returned: {responses.Count} from DB");
                return responses.Count > 0 ? Ok(responses) : StatusCode(204, "No Available Menu Items");
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Getting Menu Items");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            Log.Information("Operation of Get Menu Item by ID Has Been Started");
            try
            {
                var response = await _menuService.GetMenuItemByIdAsync(id);
                if (response == null)
                {
                    Log.Warning($"Menu Item with ID {id} not found");
                    return NotFound();
                }
                Log.Information("Menu Item Retrieved Successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Getting Menu Item by ID");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddMenuItem([FromBody] CreateMenuItemDTO itemDto)
        {
            Log.Information("Operation of Add Menu Item Has Been Started");
            try
            {
                await _menuService.AddMenuItemAsync(itemDto);
                Log.Information("Menu Item Added Successfully");
                return StatusCode(201, "Menu Item Added Successfully");
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Adding Menu Item");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateMenuItem([FromBody] UpdateMenuItemDTO itemDto)
        {
            Log.Information("Operation of Update Menu Item Has Been Started");
            try
            {
                await _menuService.UpdateMenuItemAsync(itemDto);
                Log.Information("Menu Item Updated Successfully");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Updating Menu Item");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            Log.Information("Operation of Delete Menu Item Has Been Started");
            try
            {
                await _menuService.DeleteMenuItemAsync(id);
                Log.Information($"Menu Item with ID {id} Deleted Successfully");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Deleting Menu Item");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateStock([FromQuery] int itemId, [FromQuery] int quantity)
        {
            Log.Information("Operation of Update Stock Has Been Started");
            try
            {
                await _menuService.UpdateStockAsync(itemId, quantity);
                Log.Information("Stock Updated Successfully");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Updating Stock");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
        // Order Endpoints
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> PlaceOrder([FromBody] CreateOrderDTO orderDto)
        {
            Log.Information("Operation of Place Order Has Been Started");
            try
            {
                await _orderService.PlaceOrderAsync(orderDto);
                Log.Information("Order Placed Successfully");
                return StatusCode(201, "Order Placed Successfully");
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Placing the Order");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetOrders()
        {
            Log.Information("Operation of Get Orders Has Been Started");
            try
            {
                var responses = await _orderService.GetOrdersAsync();
                Log.Information($"Orders Returned: {responses.Count} from DB");
                return responses.Count > 0 ? Ok(responses) : StatusCode(204, "No Available Orders");
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Getting Orders");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            Log.Information("Operation of Delete Order Has Been Started");
            try
            {
                await _orderService.DeleteOrderAsync(id);
                Log.Information($"Order with ID {id} Deleted Successfully");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Deleting the Order");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // Review Endpoints
        [HttpGet]
        [Route("[action]/{itemId}")]
        public async Task<IActionResult> GetReviewsForItem(int itemId)
        {
            Log.Information("Operation of Get Reviews for Item Has Been Started");
            try
            {
                var responses = await _reviewService.GetReviewsForItemAsync(itemId);
                Log.Information($"Reviews Returned: {responses.Count} from DB");
                return responses.Count > 0 ? Ok(responses) : StatusCode(204, "No Available Reviews for this Item");
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Getting Reviews for Item");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddReview([FromBody] CreateReviewDTO reviewDto)
        {
            Log.Information("Operation of Add Review Has Been Started");
            try
            {
                await _reviewService.AddReviewAsync(reviewDto);
                Log.Information("Review Added Successfully");
                return StatusCode(201, "Review Added Successfully");
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Adding the Review");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // User Endpoints
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO userDto)
        {
            Log.Information("Operation of Register User Has Been Started");
            try
            {
                var user = await _userService.RegisterUserAsync(userDto);
                Log.Information("User Registered Successfully");
                return StatusCode(201, "User Registered Successfully");
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Registering the User");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserDTO userDto)
        {
            Log.Information("Operation of Authenticate User Has Been Started");
            try
            {
                var user = await _userService.AuthenticateUserAsync(userDto);
                if (user == null)
                {
                    Log.Warning("Authentication Failed: User not found");
                    return Unauthorized();
                }
                Log.Information("User Authenticated Successfully");
                return Ok(user);
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Authenticating the User");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            Log.Information("Operation of Get User by ID Has Been Started");
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    Log.Warning($"User with ID {id} not found");
                    return NotFound();
                }
                Log.Information("User Retrieved Successfully");
                return Ok(user);
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Getting User by ID");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO userDto)
        {
            Log.Information("Operation of Update User Has Been Started");
            try
            {
                await _userService.UpdateUserAsync(userDto);
                Log.Information("User Updated Successfully");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Updating the User");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
