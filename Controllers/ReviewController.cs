using E_Restaurant.DTOs.ReviewDTO.Request;
using E_Restaurant.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace E_Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // Review Endpoints
        
        [HttpGet("[action]/{itemId}")]
        public async Task<IActionResult> GetReviewsForItem(int itemId)
        {
            Log.Information("Operation of Get Reviews for Item Has Been Started");
            try
            {
                var responses = await _reviewService.GetReviewsForItemAsync(itemId);
                Log.Information($"Reviews Returned: {responses.Count} from DB");
                return responses.Count > 0 ? Ok(responses) : StatusCode(204, "No Available Reviews for this Item");
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Getting Reviews for Item");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddReview([FromBody] CreateReviewDTO reviewDto)
        {
            Log.Information("Operation of Add Review Has Been Started");
            try
            {
                await _reviewService.AddReviewAsync(reviewDto);
                Log.Information("Review Added Successfully");
                return StatusCode(201, "Review Added Successfully");
            }
            catch (Exception ex)
            {
                Log.Error("An Error Was Occurred When Adding the Review");
                Log.Error(ex.ToString());
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}

