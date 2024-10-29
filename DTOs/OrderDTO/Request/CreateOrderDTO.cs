namespace E_Restaurant.DTOs.OrderDTO.Request
{
    public class CreateOrderDTO
    {
        public int UserId { get; set; }
        public List<int> MenuItemIds { get; set; }
    }
}
