namespace StudentGradeApp.Models
{
    public class PaystackResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public PaystackData Data { get; set; } = new PaystackData();
    }
}
