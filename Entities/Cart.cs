namespace E_Restaurant.Entities
{
    public class Cart : MainEntity
    {
        public List<MenuItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
    }
}
