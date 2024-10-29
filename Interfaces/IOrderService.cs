using E_Restaurant.DTOs.OrderDTO.Request;
using E_Restaurant.DTOs.OrderDTO.Response;
using System.Threading.Tasks;

namespace E_Restaurant.Interfaces
{
    public interface IOrderService
    {
        Task PlaceOrderAsync(CreateOrderDTO orderDto);
        Task<List<OrderDTO>> GetOrdersAsync();
        Task DeleteOrderAsync(int id);
    }
}
