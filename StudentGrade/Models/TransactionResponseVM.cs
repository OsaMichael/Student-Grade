namespace StudentGradeApp.Models
{
    public class TransactionResponseVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Amount { get; set; }
        public string TransReference { get; set; }
        public string Currency { get; set; }
        public string StudentNumber { get; set; }
        public string Remark { get; set; }
        public DateTime TransDate { get; set; }
    }
}
