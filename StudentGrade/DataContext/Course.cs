using StudentGradeApp.Models;

namespace StudentGradeApp.DataContext
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string CourseCode { get; set; } = string.Empty;
        public List<StudentCourse> StudentCourses { get; set; } = new();
    }
}
