using E_Restaurant.DTOs.OrderDTO.Request;
using E_Restaurant.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace E_Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Order Endpoints
 
        [HttpPost("[action]")]
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

        [HttpGet("[action]")]
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

        [HttpDelete("[action]/{id}")]
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
    }
}

