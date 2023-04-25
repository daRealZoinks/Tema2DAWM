using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class RepositoryBase<T> where T : BaseEntity
    {
        protected readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<T>();
        }

        public T GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(T entity)
        {
            var fervrdtbr = _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Update(entity);
        }

        public List<T> GetAll()
        {
            return GetRecords().ToList();
        }

        public bool Any(Func<T, bool> expression)
        {
            return GetRecords().Any(expression);
        }

        protected IQueryable<T> GetRecords()
        {
            return _dbSet.AsQueryable<T>();
        }
    }
}
