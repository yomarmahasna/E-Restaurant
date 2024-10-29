namespace E_Restaurant.Entities
{
    public class Menu : MainEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Category> Categories { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
    }
}
