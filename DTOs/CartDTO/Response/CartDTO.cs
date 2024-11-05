using E_Restaurant.DTOs.MenuDTO.Response;

namespace E_Restaurant.DTOs.CartDTO.Response
{
    public class CartDTO
    {
        public int Id { get; set; }
        public List<MenuItemDTO> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
    }
}
