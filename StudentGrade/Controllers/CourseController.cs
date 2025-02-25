using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentGradeApp.Models;
using StudentGradeApp.Repository;

namespace StudentGradeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IStudentGradeRepository _repository;
        private readonly ILogger<CourseController> _logger;
        public CourseController(IStudentGradeRepository repository, ILogger<CourseController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("getCourses")]
        public async Task<IActionResult> GetCourses()
        {
            var result = await _repository.GetCourses();

            return Ok(result);

        }

        [HttpPost("AddNewCourse")]
        public async Task<IActionResult> Course([FromBody] CourseModel model)
        {
            var result = await _repository.AddCourse(model);
            if (result.message == "Successful")
            {
                return Ok(result);
            }
            else
            {
                return UnprocessableEntity(new { result.message });
            }

        }

        [HttpGet("getRegisterCourses")]
        public async Task<IActionResult> GetRegisterCourses()
        {
            var result = await _repository.GetRegisterCourses();

            return Ok(result);

        }

        [HttpGet("getAllRegisteredCourses")]
        public async Task<IActionResult> GetCoursebyStudentNumber()
        {
            var result = await _repository.GetAllRegisteredCourses();
            return Ok(result);

        }


        [HttpPost("CourseRegistration")]
        public async Task<IActionResult> CourseRegistration([FromBody] CourseRegistrationModel model)
        {
            var result = await _repository.CourseRegistration(model);
            if (result.message == "Successful")
            {
                return Ok(result);
            }
            else
            {
                return UnprocessableEntity(new { result.message });
            }

        }

        [HttpGet("getCoursebyStudentNumber")]
        public async Task<IActionResult> GetCoursebyStudentNumber(string model)
        {
            var result = await _repository.GetRegisteredCourseByStudentNumber(model);
            return Ok(result);

        }


       
    }
}
