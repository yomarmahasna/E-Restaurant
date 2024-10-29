namespace E_Restaurant.Entities
{
    public class Payment : MainEntity
    {
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int? PaymentMethod { get; set; }
        public string Status { get; set; }

        public int OrderId { get; set; }
    }
}
