namespace E_Restaurant.Entities
{
    public class MenuItem : MainEntity
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }
        public int? Category { get; set; }
    }
}
