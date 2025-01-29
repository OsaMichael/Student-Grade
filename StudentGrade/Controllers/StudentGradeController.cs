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
                return BadRequest(result.message);
            }
                
        }

        [HttpGet("GetStudents")]
        public async Task<IActionResult> GetStudents()
        {
            var res = await _repository.GetStudents();
            return Ok(res);
        }
    }
    }
