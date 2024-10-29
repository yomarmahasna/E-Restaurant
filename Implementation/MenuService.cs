using E_Restaurant.Context;
using E_Restaurant.DTOs.MenuDTO.Request;
using E_Restaurant.DTOs.MenuDTO.Response;
using E_Restaurant.Entities;
using E_Restaurant.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Restaurant.Implementation
{

        public class MenuService : IMenuService
        {
            private readonly RestaurantDbContext _context;

            public MenuService(RestaurantDbContext context)
            {
                _context = context;
            }

            public async Task<List<MenuItemDTO>> GetMenuItemsAsync()
            {
                var response = from item in _context.Menus
                               select new MenuItemDTO()
                               {
                                   Id = item.Id,
                                   Name = item.Name,
                                   Description = item.Description,
                                   Price = item.Price,
                                   Stock = item.Stock
                               };
                return await response.ToListAsync();
            }

            public async Task<MenuItemDTO> GetMenuItemByIdAsync(int id)
            {
                return await _context.Menus
                    .Where(item => item.Id == id)
                    .Select(item => new MenuItemDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Price = item.Price,
                        Stock = item.Stock
                    })
                    .FirstOrDefaultAsync(); // returns null if not found
            }

            public async Task AddMenuItemAsync(CreateMenuItemDTO itemDto)
            {
                var item = new MenuItem
                {
                    Name = itemDto.Name,
                    Description = itemDto.Description,
                    Price = itemDto.Price,
                    Stock = itemDto.Stock
                };
            if (item != null)
            {
                await _context.MenuItems.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            }

            public async Task UpdateMenuItemAsync(UpdateMenuItemDTO itemDto)
            {
                var item = await _context.Menus
                    .Where(m => m.Id == itemDto.Id)
                    .Select(m => new MenuItem
                    {
                        Id = m.Id,
                        Name = itemDto.Name,
                        Description = itemDto.Description,
                        Price = itemDto.Price,
                        Stock = itemDto.Stock
                    })
                    .FirstOrDefaultAsync();

                if (item != null)
                {
                    item.Name = itemDto.Name;
                    item.Description = itemDto.Description;
                    item.Price = itemDto.Price;
                    item.Stock = itemDto.Stock;

                    _context.MenuItems.Update(item);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task DeleteMenuItemAsync(int id)
            {
                var item = await _context.Menus
                    .Where(m => m.Id == id)
                    .Select(m => new MenuItem { Id = m.Id })
                    .FirstOrDefaultAsync();

                if (item != null)
                {
                    _context.MenuItems.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task UpdateStockAsync(int itemId, int quantity)
            {
                var item = await _context.Menus
                    .Where(m => m.Id == itemId)
                    .Select(m => new MenuItem { Id = m.Id, Stock = m.Stock })
                    .FirstOrDefaultAsync();

                if (item != null)
                {
                    item.Stock -= quantity;
                    _context.MenuItems.Update(item);
                    await _context.SaveChangesAsync();
                }
            }
        }

    }

