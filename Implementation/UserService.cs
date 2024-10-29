using E_Restaurant.Context;
using E_Restaurant.DTOs.UserDTO.Request;
using E_Restaurant.DTOs.UserDTO.Response;
using E_Restaurant.Entities;
using E_Restaurant.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Restaurant.Implementation
{
    public class UserService : IUserService
    {
        private readonly RestaurantDbContext _context;

        public UserService(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<UserDTO> RegisterUserAsync(RegisterUserDTO userDto)
        {
            // Check if the user already exists
            var existingUser = await _context.Persons.FirstOrDefaultAsync(u => u.Email == userDto.Email)

            if (existingUser != null)
            {
                return null; 
            }

            // Create a new user entity
            var user = new Person
            {
                Name = userDto.Username,
                Email = userDto.Email,
                Password = HashPassword(userDto.Password), 
                Role = "Customer" 
            };

            // Add the user to the context
            _context.Persons.Add(user);
            await _context.SaveChangesAsync();

            return new UserDTO
            {
                Id = user.Id,
                Username = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<UserDTO> AuthenticateUserAsync(AuthenticateUserDTO userDto)
        {
            // Find the user by email
            var user = await _context.Persons
                .FirstOrDefaultAsync(u => u.Email == userDto.Email &&
                                           u.Password == HashPassword(userDto.Password)); // Validate password

            if (user == null)
            {
                // Handle authentication failure
                return null; // or throw an exception
            }

            return new UserDTO
            {
                Id = user.Id,
                Username = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _context.Persons.FindAsync(id);
            if (user == null) return null;

            return new UserDTO
            {
                Id = user.Id,
                Username = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task UpdateUserAsync(UserDTO userDto)
        {
            var user = await _context.Persons.FindAsync(userDto.Id);
            if (user != null)
            {
                user.Name = userDto.Username;
                user.Email = userDto.Email;


                _context.Persons.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        private string HashPassword(string password)
        {
            return password; 
        }
    }
}
