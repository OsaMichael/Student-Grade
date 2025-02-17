using System.ComponentModel.DataAnnotations;

namespace StudentGradeApp.DataContext
{
    public class StudentAccount
    {
        [Key]
        public int Id { get; set; }
        public string StudentFullName { get; set; }
        public string StudentNumber { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string State { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }
        public List<StudentCourse> StudentCourses { get; set; } = new();

    }
}
