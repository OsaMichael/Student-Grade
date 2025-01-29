using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentGradeApp.Controllers;
using StudentGradeApp.DataContext;
using StudentGradeApp.Models;

namespace StudentGradeApp.Repository
{
    public class StudentGradeRepository : IStudentGradeRepository
    {
        private readonly StudentContext _context;
        private readonly ILogger<StudentGradeRepository> _logger;
        private readonly IMapper _mapper;
        public StudentGradeRepository(StudentContext context, ILogger<StudentGradeRepository> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResponseModel> AddGrade(StudentGradeModel model)
        {
            var response = new ResponseModel();
            if (model == null)
            {
                var res = new ResponseModel(); res.message = "Value cannot be null";
                _logger.LogInformation("Request : {Request}", res);
                return res;
            }
            try
            {
                // Check if user details already exist
                var isExist = await GetStudentDetail(model.StudentNumber, model.StudentName, model.Subject);
                if (isExist == null)
                {
                    _logger.LogInformation("About to assign grade to student:{Student}", model);

                    var student = _mapper.Map<Student>(model);

                    await _context.AddAsync(student);
                    var save = await _context.SaveChangesAsync();

                    if (save == 1) {

                        var resp = response.message = "Successful";
                        response.message = resp;
                    }
                    else { var resp = response.message = "Was not successful"; response.message = resp; }

                }
                else
                {
                    var res = new ResponseModel(); res.message = "Details already exist for this student";
                    _logger.LogInformation("Request : {Request}", res);
                    return res;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("{StackTrace} an error has occur: {ex.Message}", ex.StackTrace, ex.Message);
                throw;
            }
            return response;
        }

        private async Task<object> GetStudentDetail(string id, string name, string subject)
        {
            var output = await _context.Students.FirstOrDefaultAsync(s => s.StudentNumber == id && s.StudentName == name && s.Subject == subject);
            return output;
        }

        public async Task<List<StudentGradeResponse>> GetStudents()
        {
            var res = await _context.Students.ToListAsync();
            var student = _mapper.Map<List<StudentGradeResponse>>(res);
            return student;
        }

    }
}