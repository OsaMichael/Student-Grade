namespace StudentGradeApp.Models
{
    public class PaymentResponse
    {
        //public int PaymentId { get; set; }
        //public string StudentId { get; set; } = string.Empty; // Unique identifier for the student
        public string StudentNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string PaymentMethod { get; set; } = string.Empty; // e.g., Credit Card, Bank Transfer
        public string TransactionId { get; set; } = string.Empty; // From payment gateway
        public string Status { get; set; } = "Pending";
    }
}
