namespace E_Restaurant.DTOs.OrderDTO.Request
{
    public class CreateOrderDTO
    {
        public int UserId { get; set; }
        public List<int> MenuItemIds { get; set; }

        public decimal TotalPrice { get; set; }
        public int? Status { get; set; }

        public int CustomerId { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
