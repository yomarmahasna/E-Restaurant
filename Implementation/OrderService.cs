using E_Restaurant.Context;
using E_Restaurant.DTOs.MenuDTO.Response;
using E_Restaurant.DTOs.OrderDTO.Request;
using E_Restaurant.DTOs.OrderDTO.Response;
using E_Restaurant.Entities;
using E_Restaurant.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Restaurant.Implementation
{
    public class OrderService : IOrderService
    {

        private readonly RestaurantDbContext _context;

        public OrderService(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDTO>> GetOrdersAsync()
        {
            var response = from item in _context.Orders
                           select new OrderDTO()
                           {
                               TotalPrice = item.TotalPrice,
                               Status = item.Status

                           };
            return await response.ToListAsync();
        }

 

        public async Task PlaceOrderAsync(CreateOrderDTO orderDto)
        {
            var menuItems = await _context.MenuItems.Where(m => orderDto.MenuItemIds.Contains(m.Id)).ToListAsync();
            var totalAmount = menuItems.Sum(m => m.Price);

            var order = new Order
            {
                MenuItemIds = menuItems.Select(m => m.Id).ToList(),
                CreationDate = DateTime.Now,
                CustomerId = orderDto.UserId
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
