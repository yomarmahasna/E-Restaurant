using E_Restaurant.DTOs.UserDTO.Request;
using E_Restaurant.DTOs.UserDTO.Response;

namespace E_Restaurant.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> RegisterUserAsync(RegisterUserDTO userDto);
        Task<UserDTO> AuthenticateUserAsync(AuthenticateUserDTO userDto);
        Task<UserDTO> GetUserByIdAsync(int id);
        Task UpdateUserAsync(UserDTO userDto);
    }
}
