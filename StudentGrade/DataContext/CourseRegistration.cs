using System.ComponentModel.DataAnnotations;

namespace StudentGradeApp.DataContext
{
    public class CourseRegistration
    {
        [Key]
        public int Id { get; set; }
        public string StudentNumber { get; set; }
        public string StudentName { get; set; }
        public string CourseCode { get; set; }
        public int CourseId { get; set; }
        public DateTime DateOfReg { get; set; } 
    }
}
