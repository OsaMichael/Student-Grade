using StudentGradeApp.DataContext;

namespace StudentGradeApp.Models
{
    public class StudentCourseModel
    {
       // public int StudentId { get; set; }
        public string StudentNumber { get; set; }
       // public StudentAccount Student { get; set; } = null!;
        public DateTime DateOfReg { get; set; } 

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }

    public class CourseRegistrationModel
    {
        public string StudentNumber { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }

        //public int CourseId { get; set; }
        //public Course Course { get; set; } = null!;
        //public DateTime DateOfReg { get; set; } = DateTime.Now;
    }

    public class StudentCourseResponse
    {
        public int StudentId { get; set; }
        public string StudentNumber { get; set; }
        //public StudentAccount Student { get; set; } = null!;
        public DateTime DateOfReg { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }

    public class RegisteredCourseResponse
    {
        public string StudentNumber { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string DateOfReg { get; set; }

    }
}
