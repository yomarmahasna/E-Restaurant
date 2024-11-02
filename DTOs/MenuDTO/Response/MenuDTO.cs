namespace E_Restaurant.DTOs.MenuDTO.Response
{
    public class MenuDTO
    {
        public int Id { get; set; } // Optional, include if you want to reference a specific Menu
        public string Title { get; set; } // Name of the menu item
        public string Description { get; set; } // Description of the menu item
        public List<int> CategoryIds { get; set; } // List of category IDs associated with the menu item
        public DateTime CreationDate { get; set; } // The date when the menu item was created

    }
}
