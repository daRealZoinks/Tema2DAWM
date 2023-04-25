using DataLayer.Dtos;
using DataLayer.Entities;

namespace Core.Dtos
{
    public class GradesByStudent
    {
        public int? StudentId { get; set; }
        public string StudentCompleteName { get; set; }

        public List<GradeDto> Grades { get; set; } = new();

        public GradesByStudent(Student student)
        {
            StudentId = student?.Id;
            StudentCompleteName = $"{student?.FirstName} {student?.LastName}";

            if (student?.Grades != null)
            {
                Grades = student.Grades
                    .Select(x => new GradeDto
                    {
                        Value = x.Value,
                        Course = x.Course,
                    }).ToList();
            }
        }
    }
}