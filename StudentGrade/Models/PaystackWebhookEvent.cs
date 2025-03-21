namespace StudentGradeApp.Models
{
    public class PaystackWebhookEvent
    {
        public string @event { get; set; }
        public PaystackDataNew data { get; set; }
    }

    public class PaystackDataNew
    {
        public string reference { get; set; }
        public string status { get; set; }
        public string subscription_code { get; set; }
    }
}
