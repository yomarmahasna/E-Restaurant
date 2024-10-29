namespace E_Restaurant.Entities
{
    public class Review : MainEntity
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public int MenuItemId { get; set; }

        public int CustomerId { get; set; }



    }
}
