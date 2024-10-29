namespace E_Restaurant.DTOs.MenuDTO.Request
{
    public class CreateMenuItemDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
