using StudentGradeApp.Models;

namespace StudentGradeApp.Repository
{
    public interface IStudentGradeRepository
    {
        public Task<ResponseModel> AddGrade(StudentGradeModel student);
        public Task<ResponseModel> EditStudent(StudentEditGradeModel model);
        public Task<List<StudentGradeResponse>> GetStudents();
        public  Task<StudentEditGradeModel> GetStudentByNumber(string number);
        public Task<ResponseModel> DeleteStudent(string number);
        public  Task<ResponseModel> AddNewStudent(RegisterStudentModel model);
        public  Task<List<StudentResponse>> GetAllStudents();
        public Task<ResponseModel> UpdateStudent(UpdateStudentModel model);
        public Task<ResponseModel> RemoveStudent(string number);
        public Task<ResponseModel> AddCourse(CourseModel model);
        public Task<List<CourseResponse>> GetCourses();
        public Task<List<StudentCourseResponse>> GetRegisterCourses();
        public Task<ResponseModel> CourseRegistration(CourseRegistrationModel model);
    }
}
