using Core.Dtos;
using DataLayer;
using DataLayer.Dtos;
using DataLayer.Entities;
using DataLayer.Enums;
using DataLayer.Mapping;

namespace Core.Services
{
    public class StudentService
    {
        private readonly UnitOfWork _unitOfWork;

        public StudentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public StudentAddDto AddStudent(StudentAddDto payload)
        {
            if (payload == null)
            {
                return null;
            }

            var existingClass = _unitOfWork.ClassesRepository.GetById(payload.ClassId);

            if (existingClass == null)
            {
                return null;
            }

            Student newStudent = new()
            {
                FirstName = payload.FirstName,
                LastName = payload.LastName,
                DateOfBirth = payload.DateOfBirth,
                Address = payload.Address,

                ClassId = existingClass.Id,
            };

            _unitOfWork.StudentsRepository.Insert(newStudent);
            _unitOfWork.SaveChanges();

            return payload;
        }

        public List<Student> GetAll()
        {
            var results = _unitOfWork.StudentsRepository.GetAll();

            return results;
        }

        public StudentDto GetById(int sudentId)
        {
            var student = _unitOfWork.StudentsRepository.GetById(sudentId);
            var result = student.ToStudentDto();
            return result;
        }

        public bool EditName(StudentUpdateDto payload)
        {
            if (payload == null || payload.FirstName == null || payload.LastName == null)
            {
                return false;
            }

            var result = _unitOfWork.StudentsRepository.GetById(payload.Id);

            if (result == null)
            {
                return false;
            }

            result.FirstName = payload.FirstName;
            result.LastName = payload.LastName;

            return true;
        }

        public GradesByStudent GetGradesById(int studentId, CourseType courseType)
        {
            var studentWithGrades = _unitOfWork.StudentsRepository.GetByIdWithGrades(studentId, courseType);

            var result = new GradesByStudent(studentWithGrades);

            return result;
        }

        public List<string> GetClassStudents(int classId)
        {
            var students = _unitOfWork.StudentsRepository.GetClassStudents(classId);

            return students;
        }

        public Dictionary<int, List<Student>> GetGroupedStudents()
        {
            var results = _unitOfWork.StudentsRepository.GetGroupedStudents();

            return results;
        }
    }
}
