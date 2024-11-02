namespace E_Restaurant.Entities
{
    public class Menu : MainEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<MenuCategory> MenuCategories { get; set; } = new List<MenuCategory>();
    }
}
