using System.ComponentModel.DataAnnotations;

namespace StudentGradeApp.DataContext
{
    public class PaystackPayment
    {
       // [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; } = string.Empty;
        public int Amount { get; set; } // Amount in kobo (NGN 100 = 10000)
        public string Currency { get; set; } = "NGN";
        public string CallbackUrl { get; set; } = string.Empty; 
        public string TrxRef { get; set; } = string.Empty; 
        public string Name { get; set; } = string.Empty; 
        public bool Status { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }

    public class PaystackPaymentModel
    {
        public string Email { get; set; } = string.Empty;
        public int Amount { get; set; } // Amount in kobo (NGN 100 = 10000)
        public string Name { get; set; } = string.Empty;

    }
}
