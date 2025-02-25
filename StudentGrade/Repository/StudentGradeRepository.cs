using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudentGradeApp.Controllers;
using StudentGradeApp.DataContext;
using StudentGradeApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private async Task<object> GetAccountDetails(string name, DateTime age, string department)
        {
            var output = await _context.StudentAccounts.FirstOrDefaultAsync(s => s.StudentFullName == name && s.DateOfBirth == age && s.Department == department );
            return output;
        }

        public async Task<List<StudentGradeResponse>> GetStudents()
        {
            var res = await _context.Students.ToListAsync();
            var student = _mapper.Map<List<StudentGradeResponse>>(res);
            return student;
        }

        public async Task<ResponseModel> EditStudent(StudentEditGradeModel model)
        {
            var response = new ResponseModel();
            var res = await _context.Students.FirstOrDefaultAsync(x => x.StudentNumber == model.StudentNumber);
            if (res != null)
            {
                res.StudentNumber = model.StudentNumber;
                res.StudentName = model.StudentName;
                res.Subject = model.Subject;
                res.Remark = model.Remark;
                res.Grade = model.Grade;
                 _context.Update(res);
              await  _context.SaveChangesAsync();
                 response.message = "Success";
            }
            else
            {
                response.message = "Update Fail";
            }

            return response;
        }

        public async Task<StudentEditGradeModel> GetStudentByNumber(string number)
        {
            var response = new StudentEditGradeModel();
            var res = await _context.Students.FirstOrDefaultAsync(x=>x.StudentNumber == number);
            if (res != null)
            {
                var result = _mapper.Map<StudentEditGradeModel>(res);
                response = result;
            }
            return response;
        }

        public async Task<ResponseModel> DeleteStudent(string number)
        {
            var response = new ResponseModel();
            var res = await _context.Students.FirstOrDefaultAsync(x => x.StudentNumber == number);
            if (res != null)
            {
               _context.Students.Remove(res);
               await _context.SaveChangesAsync();

                response.message = "deleted";
            }
            return response;
        }

        public async Task<ResponseModel> RemoveStudent(string number)
        {
            var response = new ResponseModel();
            var res = await _context.StudentAccounts.FirstOrDefaultAsync(x => x.StudentNumber == number);
            if (res != null)
            {
                _context.StudentAccounts.Remove(res);
                await _context.SaveChangesAsync();

                response.message = "deleted";
            }
            return response;
        }

        public async Task<ResponseModel> AddNewStudent(RegisterStudentModel model)
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
                var isExist = await GetAccountDetails(model.StudentFullName, model.DateOfBirth,  model.Department);
                if (isExist == null)
                {
                    // auto generate student Number

                    var assignStudentId = await GenerateStudentNumber();
                    var insert = new StudentAccount 
                    { 
                          StudentNumber = assignStudentId,
                          Address = model.Address,
                          DateOfBirth = model.DateOfBirth,
                          Department = model.Department,
                          Faculty = model.Faculty,
                          Phone = model.Phone,
                          State = model.State,
                          StudentFullName = model.StudentFullName,                                              
                    };
                    _logger.LogInformation("About to add new student:{Student}", model);

                    await _context.AddAsync(insert);
                    var save = await _context.SaveChangesAsync();

                    if (save == 1)
                    {

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

        public async Task<string> GenerateStudentNumber()
        {
            string prefix = "STU";
            string lastId = await GetStudentLastStudentId();
            string numericPart = lastId.Substring(3);
            int number = int.Parse(numericPart); 
            number++;

            string studentId = number.ToString("D7");

            return $"{prefix}{studentId}";
        }
        private async Task<string> GetStudentLastStudentId()
        {
            var lastStudent = await _context.StudentAccounts.OrderByDescending(x => x.StudentNumber).FirstOrDefaultAsync();

            return lastStudent?.StudentNumber ?? "STU0000000";
        }
        public async Task<List<StudentResponse>> GetAllStudents()
        {
            var res = await _context.StudentAccounts.ToListAsync();
            var student = _mapper.Map<List<StudentResponse>>(res);
            return student;
        }

        public async Task<List<CourseResponse>> GetCourses()
        {
            var res = await _context.Courses.ToListAsync();
            var student = _mapper.Map<List<CourseResponse>>(res);
            return student;
        }
        public async Task<List<StudentCourseResponse>> GetRegisterCourses()
        {
            var res = await _context.StudentCourses.ToListAsync();
            var student = _mapper.Map<List<StudentCourseResponse>>(res);
            return student;
        }

        public async Task<ResponseModel> UpdateStudent(UpdateStudentModel model)
        {
            var response = new ResponseModel();
            var res = await _context.StudentAccounts.FirstOrDefaultAsync(x => x.StudentNumber == model.StudentNumber);
            if (res != null)
            {
                res.StudentNumber = model.StudentNumber;
                res.StudentFullName = model.StudentFullName;
                res.Phone = model.Phone;
                res.Address = model.Address;
                res.State = model.State;
                res.Faculty = model.Faculty;
                res.Department = model.Department;
                res.DateOfBirth = model.DateOfBirth;
                _context.Update(res);
                await _context.SaveChangesAsync();
                response.message = "Success";
            }
            else
            {
                response.message = "Update Fail";
            }

            return response;
        }

        /// courses
        /// 
        public async Task<ResponseModel> AddCourse(CourseModel model)
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
                var isExist = await GetCourse(model.CourseName, model.CourseCode);
                if (isExist == null)
                {
                    var course = _mapper.Map<Course>(model);
                 
                    _logger.LogInformation("About to add new student:{Student}", model);

                    await _context.AddAsync(course);
                    var save = await _context.SaveChangesAsync();

                    if (save == 1)
                    {

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

        public async Task<ResponseModel> CourseRegistration(CourseRegistrationModel model)
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
                var isExist = await GetRegisteredCourse(model.StudentNumber, model.CourseCode);
                if (isExist == null)
                {

                    var reg = new CourseRegistration
                    {
                        CourseCode = model.CourseCode,
                        CourseName = model.CourseName,
                        StudentNumber = model.StudentNumber,
                        DateOfReg = DateTime.Now,
                        StudentName = ""
                    };

                    _logger.LogInformation("About to add new student:{Student}", model);

                    await _context.AddAsync(reg);
                    var save = await _context.SaveChangesAsync();

                    if (save == 1)
                    {

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

        private async Task<object> GetCourse(string name, string code)
        {
            var output = await _context.Courses.FirstOrDefaultAsync(s => s.CourseName == name && s.CourseCode  == code);
            return output;
        }

        private async Task<object> GetRegisteredCourse(string StudentNumber, string courseCode)
        {
            var output = await _context.CourseRegistrations.FirstOrDefaultAsync(s => s.StudentNumber == StudentNumber && s.CourseCode == courseCode);
            return output;
        }

        public async Task<List<RegisteredCourseResponse>> GetRegisteredCourseByStudentNumber(string StudentNumber)
        {
            var output = await _context.CourseRegistrations.FirstOrDefaultAsync(s => s.StudentNumber == StudentNumber);
            var student = _mapper.Map<List<RegisteredCourseResponse>>(output);
            return student;
        }


        public async Task<List<RegisteredCourseResponse>> GetAllRegisteredCourses()
        {
            var res = await _context.CourseRegistrations.ToListAsync();
            var registeredCourses = new List<RegisteredCourseResponse>(); // Create a list to hold responses

            foreach (var dd in res)
            {
                var rr = new RegisteredCourseResponse
                {
                    StudentNumber = dd.StudentNumber,
                    CourseCode = dd.CourseCode,
                    CourseName = dd.CourseName,  
                    DateOfReg = dd.DateOfReg.ToString("yyyy-MM-dd") // Formatting date
                };

                registeredCourses.Add(rr); // Add each course to the list
            }

            return registeredCourses; // Return the list
        }

        public async Task<ResponseModel> Payment(PaymentModel model)
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
                // Simulate payment gateway processing (replace with real integration)
                string transactionId = Guid.NewGuid().ToString();
                string paymentStatus = "Completed"; // In real-world, check gateway response

                var payment = new Payment
                {
                    StudentNumber = model.StudentNumber,
                    Amount = model.Amount,
                    PaymentMethod = model.PaymentMethod,
                    TransactionId = transactionId,
                    Status = paymentStatus,
                    PaymentDate = DateTime.UtcNow
                };

                _context.Payments.Add(payment);
               var save = await _context.SaveChangesAsync();
                if (save == 1)
                {

                    var resp = response.message = "Successful";
                    response.message = resp;
                }
                else { var resp = response.message = "Was not successful"; response.message = resp; }
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError("{StackTrace} an error has occur: {ex.Message}", ex.StackTrace, ex.Message);
                throw;
            }

        }

        public async Task<List<PaymentResponse>> GetPaymentHistoryByStudentNumber(string StudentNumber)
        {
            var output = await _context.Payments.Where(s => s.StudentNumber == StudentNumber).OrderByDescending(p => p.PaymentDate)
            .ToListAsync();
            var student = _mapper.Map<List<PaymentResponse>>(output);
            return student;
        }

        public async Task<List<PaymentResponse>> GetAllPayments()
        {
            var res = await _context.Payments.ToListAsync();
            var student = _mapper.Map<List<PaymentResponse>>(res);
            return student;
        }
    }
}