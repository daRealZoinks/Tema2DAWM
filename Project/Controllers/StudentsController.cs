using Core.Dtos;
using Core.Services;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentsController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("add")]
        public IActionResult Add(StudentAddDto payload)
        {
            var result = _studentService.AddStudent(payload);

            if (result == null)
            {
                return BadRequest("Student cannot be added");
            }

            return Ok(result);
        }

        [HttpGet("get-all")]
        public ActionResult<List<Student>> GetAll()
        {
            var results = _studentService.GetAll();
            return Ok(results);
        }

        [HttpGet("get/{studentId}")]
        public ActionResult<Student> GetById(int studentId)
        {
            var result = _studentService.GetById(studentId);

            if (result == null)
            {
                return BadRequest("Student not found");
            }

            return Ok(result);
        }

        [HttpPatch("edit-name")]
        public ActionResult<bool> GetById([FromBody] StudentUpdateDto studentUpdateModel)
        {
            var result = _studentService.EditName(studentUpdateModel);

            if (!result)
            {
                return BadRequest("Student could not be updated");
            }

            return result;
        }

        [HttpPost("grades-by-course")]
        public ActionResult<GradesByStudent> GetCourseGradesByStudentId([FromBody] StudentGradesRequest request)
        {
            var result = _studentService.GetGradesById(request.StudentId, request.CourseType);
            return Ok(result);
        }

        [HttpGet("{classId}/class-students")]
        public IActionResult GetClassStudents([FromRoute] int classId)
        {
            var results = _studentService.GetClassStudents(classId);
            return Ok(results);
        }

        [HttpGet("grouped-students")]
        public IActionResult GetGroupedStudents()
        {
            var results = _studentService.GetGroupedStudents();
            return Ok(results);
        }
    }
}
