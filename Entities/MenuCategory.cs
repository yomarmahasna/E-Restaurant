namespace E_Restaurant.Entities
{
    public class MenuCategory
    {
        public int MenuId { get; set; }
        public Menu Menu { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
