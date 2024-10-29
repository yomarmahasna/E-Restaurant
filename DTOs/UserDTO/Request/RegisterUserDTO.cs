namespace E_Restaurant.DTOs.UserDTO.Request
{
    public class RegisterUserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? Role { get; set; }

    }
}
