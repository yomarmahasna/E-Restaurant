namespace E_Restaurant.Entities
{
    public class Person : MainEntity
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? Role { get; set; }
        public string Password { get; set; }
    }
}
