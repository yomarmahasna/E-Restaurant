namespace E_Restaurant.Entities
{
    public class TableReservation : MainEntity
    {
        public int NumberOfGuests { get; set; }
        public string TableNumber { get; set; }
        public string Status { get; set; }
        public int CustomerId { get; set; }
    }
}
