using DataLayer.Dtos;
using DataLayer.Entities;

namespace DataLayer.Mapping
{
    public static class StudentsMappingExtentions
    {
        public static List<StudentDto> ToStudentDtos(this List<Student> students)
        {
            if (students == null)
            {
                return null;
            }

            var results = students.Select(x => x.ToStudentDto()).ToList();
            return results;
        }


        public static StudentDto ToStudentDto(this Student student)
        {
            if (student == null)
            {
                return null;
            }

            StudentDto result = new()
            {
                Id = student.Id,
                FullName = $"{student.FirstName} {student.LastName}",
                ClassId = student.ClassId,
                ClassName = student.Class?.Name,
                Grades = student.Grades.ToGradeDtos()
            };

            return result;
        }

    }
}
