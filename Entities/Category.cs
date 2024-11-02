namespace E_Restaurant.Entities
{
    public class Category : MainEntity
    {
        public string Description { get; set; }
        public ICollection<MenuCategory> MenuCategories { get; set; } = new List<MenuCategory>();

    }
}
