namespace E_Restaurant.DTOs.ReviewDTO.Response
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
