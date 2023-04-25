using Core.Dtos;
using Core.Services;
using DataLayer.Entities;
using DataLayer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/students")]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _studentService;
        private readonly UserService _userService;

        public StudentsController(StudentService studentService, UserService userService)
        {
            _studentService = studentService;
            _userService = userService;
        }

        [HttpPost("add")]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public ActionResult<List<Student>> GetAll()
        {
            var results = _studentService.GetAll();
            return Ok(results);
        }

        [HttpGet("get/{studentId}")]
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
        public ActionResult<GradesByStudent> GetCourseGradesByStudentId([FromBody] StudentGradesRequest request)
        {
            var result = _studentService.GetGradesById(request.StudentId, request.CourseType);
            return Ok(result);
        }

        [HttpGet("{classId}/class-students")]
        [AllowAnonymous]
        public IActionResult GetClassStudents([FromRoute] int classId)
        {
            var results = _studentService.GetClassStudents(classId);
            return Ok(results);
        }

        [HttpGet("grouped-students")]
        [AllowAnonymous]
        public IActionResult GetGroupedStudents()
        {
            var results = _studentService.GetGroupedStudents();
            return Ok(results);
        }

        [HttpGet("get-all-students-grades")]
        [Authorize(Roles = "Teacher")]
        public IActionResult GetGrades()
        {
            var results = _studentService.GetAllStudentsGrades();
            return Ok(results);
        }

        [HttpGet("get-student-grades")]
        [Authorize(Roles = "Student")]
        public ActionResult<GradesByStudent> GetStudentGrades()
        {
            var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            GradesByStudent result = null;

            foreach (var item in Enum.GetValues(typeof(CourseType)))
            {
                result = _studentService.GetGradesById(int.Parse(id), CourseType.Math);
            }

            if (result == null)
            {
                return BadRequest("Student not found");
            }

            return result;
        }
    }
}
