using E_Restaurant.DTOs.MenuDTO.Response;

namespace E_Restaurant.DTOs.OrderDTO.Response
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<MenuItemDTO> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public int? Status { get; set; }
    }
}
