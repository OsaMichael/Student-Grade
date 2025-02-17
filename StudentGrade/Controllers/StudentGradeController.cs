using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentGradeApp.Models;
using StudentGradeApp.Repository;

namespace StudentGradeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentGradeController : ControllerBase
    {
        private readonly IStudentGradeRepository _repository;
        private readonly ILogger<StudentGradeController> _logger;
        public StudentGradeController(IStudentGradeRepository repository, ILogger<StudentGradeController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost("AddGrade")]
        public async Task<IActionResult> AddGrade([FromBody] StudentGradeModel studentGrade)
        {
                var result = await _repository.AddGrade(studentGrade);
            if (result.message == "Successful")
            {
                return RedirectToAction("GetStudents");
            }
            else 
            {
                return UnprocessableEntity(new { result.message });
            }
                
        }

        [HttpGet("GetStudents")]
        public async Task<IActionResult> GetStudents()
        {
            var res = await _repository.GetStudents();
            return Ok(res);
        }

        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(StudentEditGradeModel model)
        {
            var res = await _repository.EditStudent(model);
            return Ok(res);
        }

        [HttpGet("getStudentByNumber")]
        public async Task<IActionResult> GetStudentByNumber(string  studentNumber)
        {
            var res = await _repository.GetStudentByNumber(studentNumber);
            return Ok(res);
        }


        [HttpDelete("deleteStudent/{studentNumber}")]
        public async Task<IActionResult> DeleteStudent(string studentNumber)
        {
            var res = await _repository.DeleteStudent(studentNumber);
            return Ok(res);
        }

        

    }
    }
