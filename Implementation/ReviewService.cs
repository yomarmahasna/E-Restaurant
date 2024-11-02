using E_Restaurant.Context;
using E_Restaurant.DTOs.ReviewDTO.Request;
using E_Restaurant.DTOs.ReviewDTO.Response;
using E_Restaurant.Entities;
using E_Restaurant.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Restaurant.Implementation
{
    public class ReviewService : IReviewService
    {
        private readonly RestaurantDbContext _context;

        public ReviewService(RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task AddReviewAsync(CreateReviewDTO reviewDto)
        {
            var item = new Review
            {

                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                ReviewDate = reviewDto.ReviewDate,
                MenuItemId = reviewDto.MenuItemId,
                CustomerId = reviewDto.CustomerId,
                Id = reviewDto.Id,
                Name = reviewDto.Name,
                IsActive = reviewDto.IsActive,
                CreationDate = reviewDto.CreationDate,
            };
            if (item != null)
            {
                await _context.Reviews.AddAsync(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ReviewDTO>> GetReviewsForItemAsync(int itemId)
        {
            var response = from review in _context.Reviews
                           select new ReviewDTO()
                           {
                               Id = review.Id,
                               MenuItemId = review.MenuItemId,
                               Rating = review.Rating,
                               Comment = review.Comment,
                           };
            return await response.ToListAsync();
        }
    }
}
