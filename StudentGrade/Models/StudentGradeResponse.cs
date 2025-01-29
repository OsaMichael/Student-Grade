namespace StudentGradeApp.Models
{
    public class StudentGradeResponse
    {
        public string StudentName { get; set; } = string.Empty;
        public string StudentNumber { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
    }
}
