namespace E_Restaurant.DTOs.ReviewDTO.Request
{
    public class CreateReviewDTO
    {
        public int MenuItemId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }

        public int CustomerId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
