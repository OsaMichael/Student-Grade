using StudentGradeApp.Models;

namespace StudentGradeApp.Repository
{
    public interface IStudentGradeRepository
    {
        public Task<ResponseModel> AddGrade(StudentGradeModel student);
        // public  Task<IEnumerable<StudentGradeResponse>> GetStudents();
        public Task<List<StudentGradeResponse>> GetStudents();
    }
}
