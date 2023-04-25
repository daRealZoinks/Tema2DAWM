using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class ClassesRepository : RepositoryBase<Class>
    {
        public ClassesRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public List<Class> GetAllWithStudentCount()
        {
            return GetRecords()
                .Include(x => x.Students)
                .Select(x => new Class
                {
                    Id = x.Id,
                    Name = x.Name,
                    StudentCount = x.Students.Count,
                }).ToList();
        }
    }
}
