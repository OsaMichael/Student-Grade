namespace StudentGradeApp.Models
{
    public class PaymentModel
    {
        public string StudentNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
    }
}
