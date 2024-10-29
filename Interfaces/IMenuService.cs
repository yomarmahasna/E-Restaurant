using E_Restaurant.DTOs.MenuDTO.Request;
using E_Restaurant.DTOs.MenuDTO.Response;

namespace E_Restaurant.Interfaces
{
    public interface IMenuService
    {
        Task<List<MenuItemDTO>> GetMenuItemsAsync();
        Task<MenuItemDTO> GetMenuItemByIdAsync(int id);
        Task AddMenuItemAsync(CreateMenuItemDTO itemDto);
        Task UpdateMenuItemAsync(UpdateMenuItemDTO itemDto);
        Task DeleteMenuItemAsync(int id);
        Task UpdateStockAsync(int itemId, int quantity);
    }
}
