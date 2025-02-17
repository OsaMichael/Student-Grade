using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentGradeApp.Models;
using StudentGradeApp.Repository;

namespace StudentGradeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentGradeRepository _repository;
        private readonly ILogger<StudentController> _logger;
        public StudentController(IStudentGradeRepository repository, ILogger<StudentController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet("AllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _repository.GetAllStudents();
        
            return Ok(result);
        
        }

        [HttpPost("RegisterStudent")]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterStudentModel model)
        {
            var result = await _repository.AddNewStudent(model);
            if (result.message == "Successful")
            {
                return RedirectToAction("AllStudents");
            }
            else
            {
                return UnprocessableEntity(new { result.message });
            }

        }
        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(UpdateStudentModel model)
        {
            var res = await _repository.UpdateStudent(model);
            return Ok(res);
        }

        [HttpDelete("removeStudent/{studentNumber}")]
        public async Task<IActionResult> RemoveStudent(string studentNumber)
        {
            var res = await _repository.RemoveStudent(studentNumber);
            return Ok(res);
        }


        [HttpPost("addNewCourse")]
        public async Task<IActionResult> Course([FromBody] CourseModel model)
        {
            var result = await _repository.AddCourse(model);
            if (result.message == "Successful")
            {
                return RedirectToAction("getCourses");
            }
            else
            {
                return UnprocessableEntity(new { result.message });
            }

        }
    }
}
 