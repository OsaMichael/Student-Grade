using System.ComponentModel.DataAnnotations;

namespace StudentGradeApp.DataContext
{
    public class StudentCourse
    {
       // [Key]
        public int StudentId { get; set; }
        public string StudentNumber { get; set; }
        public int Id { get; set; }
        public StudentAccount Student { get; set; } = null!;

        
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public DateTime DateOfReg { get; set; }
    }
}
