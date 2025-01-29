using System.ComponentModel.DataAnnotations;

namespace StudentGradeApp.DataContext
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string StudentNumber { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
    }
}
