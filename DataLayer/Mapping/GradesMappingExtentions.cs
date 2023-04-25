using DataLayer.Dtos;
using DataLayer.Entities;

namespace DataLayer.Mapping
{
    public static class GradesMappingExtentions
    {
        public static List<GradeDto> ToGradeDtos(this List<Grade> grades)
        {
            if (grades == null)
            {
                return null;
            }

            var results = grades.Select(x => x.ToGradeDto()).ToList();

            return results;
        }

        public static GradeDto ToGradeDto(this Grade grade)
        {
            if (grade == null)
            {
                return null;
            }

            GradeDto result = new()
            {
                Value = grade.Value,
                Course = grade.Course
            };

            return result;
        }
    }
}
