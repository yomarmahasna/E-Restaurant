using E_Restaurant.DTOs.MenuDTO.Request;
using E_Restaurant.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace E_Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        // Menu Endpoints
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMenu()
        {
            Log.Information("Operation of Get Menu Has Been Started");
            try
            {
                var responses = await _menuService.GetMenuAsync();
                Log.Information($"Menu Items Returned: {responses.Count} from DB");
                return responses.Count > 0 ? Ok(responses) : StatusCode(204, "No Available Menu");
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Getting Menu Items");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }


        [HttpGet("[action]")]
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

        [HttpGet("[action]/{id}")]
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


        [HttpPost("[action]")]
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

 
        [HttpPut("[action]")]
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
 
        [HttpDelete("[action]/{id}")]
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
 
        [HttpPut("[action]")]
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
    }
}

