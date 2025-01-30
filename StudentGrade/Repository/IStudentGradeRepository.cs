using StudentGradeApp.Models;

namespace StudentGradeApp.Repository
{
    public interface IStudentGradeRepository
    {
        public Task<ResponseModel> AddGrade(StudentGradeModel student);
        public Task<ResponseModel> EditStudent(StudentEditGradeModel model);
        public Task<List<StudentGradeResponse>> GetStudents();
        public  Task<StudentEditGradeModel> GetStudentByNumber(string number);
    }
}
