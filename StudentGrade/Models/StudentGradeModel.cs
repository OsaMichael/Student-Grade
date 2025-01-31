using System.ComponentModel.DataAnnotations;

namespace StudentGradeApp.Models
{
    public class StudentGradeModel
    {
        [Required]
        public string StudentName { get; set; } = string.Empty;
        [Required]
        public string StudentNumber { get; set; }
        [Required]
        public string Subject { get; set; } = string.Empty;
        [Required]
        public string Grade { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
    }
}
