using DataLayer.Entities;
using DataLayer.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class StudentsRepository : RepositoryBase<Student>
    {
        public StudentsRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public Student GetByIdWithGrades(int studentId, CourseType courseType)
        {
            var result = _appDbContext.Students
                .Select(x => new Student
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ClassId = x.ClassId,
                    Grades = x.Grades
                    .Where(g => g.Course == courseType)
                    .OrderByDescending(g => g.Value)
                    .ToList()
                }).FirstOrDefault(x => x.Id == studentId);

            return result;
        }

        public List<string> GetClassStudents(int classId)
        {
            var results = _appDbContext.Students
                .Include(x => x.Grades.Where(x => x.Value > 5))
                .Where(x => x.ClassId == classId)
                .OrderByDescending(x => x.FirstName)
                .ThenByDescending(x => x.LastName)
                .Select(x => $"{x.FirstName} {x.LastName}")
                .ToList();

            return results;
        }

        public Dictionary<int, List<Student>> GetGroupedStudents()
        {
            var results = _appDbContext.Students
                .GroupBy(x => x.ClassId)
                .Select(x => new
                {
                    ClassId = x.Key,
                    Students = x.ToList()
                })
                .ToDictionary(x => x.ClassId, x => x.Students);

            return results;
        }
    }
}
