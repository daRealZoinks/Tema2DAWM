using DataLayer.Entities;

namespace DataLayer.Repositories
{
    public class UsersRepository : RepositoryBase<User>
    {
        public UsersRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public User GetByEmail(string email)
        {
            return _appDbContext.Users.FirstOrDefault(x => x.Email == email);
        }
    }
}
