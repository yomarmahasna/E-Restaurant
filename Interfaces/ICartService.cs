using E_Restaurant.DTOs.CartDTO.Response;

namespace E_Restaurant.Interfaces
{
    public interface ICartService
    {

        Task<CartDTO> GetCartByUserIdAsync(int customerId);

        Task AddToCartAsync(int customerId, int menuItemId, int quantity);

        Task ClearCartAsync(int customerId);

        Task RemoveItemFromCartAsync(int customerId, int menuItemId);

    }
}
