using E_Restaurant.DTOs.UserDTO.Request;
using E_Restaurant.DTOs.UserDTO.Response;
using E_Restaurant.Implementation;
using E_Restaurant.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace E_Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController( IUserService userService)
        {

            _userService = userService;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO userDto)
        {
            Log.Information("Starting registration process...");
            try
            {
                var user = await _userService.RegisterUserAsync(userDto);
                if (user == null)
                {
                    return Conflict("User already exists.");
                }
                Log.Information("User registered successfully.");
                return StatusCode(201, "User registered successfully.");
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred during user registration.", ex);
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
                return Ok(new { Token = user.Token, Message = "User Authenticated Successfully" });
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Authenticating the User");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            Log.Information($"Retrieving user with ID {id}...");
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    Log.Warning($"User with ID {id} not found.");
                    return NotFound();
                }
                Log.Information("User retrieved successfully.");
                return Ok(user);
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred when retrieving user by ID.", ex);
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO userDto)
        {
            Log.Information("Starting user update process...");
            try
            {
                await _userService.UpdateUserAsync(userDto);
                Log.Information("User updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred during user update.", ex);
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
