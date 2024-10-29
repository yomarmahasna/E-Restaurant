using E_Restaurant.DTOs.ReviewDTO.Request;
using E_Restaurant.DTOs.ReviewDTO.Response;

namespace E_Restaurant.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewDTO>> GetReviewsForItemAsync(int itemId);
        Task AddReviewAsync(CreateReviewDTO reviewDto);
    }
}
