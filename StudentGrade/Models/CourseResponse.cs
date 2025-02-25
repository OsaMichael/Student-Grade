namespace StudentGradeApp.Models
{
    public class CourseResponse
    {
        public string CourseName { get; set; } = string.Empty;
        public string CourseCode { get; set; } = string.Empty;
        public int CourseUnit { get; set; }
        //public List<StudentCourseModel> StudentCourses { get; set; } = new();
    }
}
