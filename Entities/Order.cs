
namespace E_Restaurant.Entities
{
    public class Order : MainEntity
    {
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }

        public int CustomerId { get; set; }
        public List<int>? MenuItemIds { get; set; }

    }
}