using DataLayer.Repositories;

namespace DataLayer
{
    public class UnitOfWork
    {
        public StudentsRepository StudentsRepository { get; }
        public ClassesRepository ClassesRepository { get; }
        public UsersRepository UsersRepository { get; }

        private readonly AppDbContext _appDbContext;

        public UnitOfWork(StudentsRepository studentsRepository, ClassesRepository classesRepository, UsersRepository usersRepository, AppDbContext appDbContext)
        {
            StudentsRepository = studentsRepository;
            ClassesRepository = classesRepository;
            UsersRepository = usersRepository;
            _appDbContext = appDbContext;
        }

        public void SaveChanges()
        {
            try
            {
                _appDbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error saving changes to database:");
                Console.WriteLine();
                Console.WriteLine(exception.Message);
                Console.WriteLine();
                Console.WriteLine(exception.InnerException);
                Console.WriteLine();
                Console.WriteLine(exception.StackTrace);
            }
        }
    }
}
