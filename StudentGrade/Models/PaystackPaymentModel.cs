namespace StudentGradeApp.Models
{
    public class PaystackPaymentModel
    {
        public string StudentNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Amount { get; set; } // Amount in kobo (NGN 100 = 10000)
        public string Name { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;

    }
}
