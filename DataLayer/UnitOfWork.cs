using DataLayer.Repositories;

namespace DataLayer
{
    public class UnitOfWork
    {
        public StudentsRepository StudentsRepository { get; }
        public ClassesRepository ClassesRepository { get; }

        private readonly AppDbContext _appDbContext;

        public UnitOfWork(StudentsRepository studentsRepository, ClassesRepository classesRepository, AppDbContext appDbContext)
        {
            StudentsRepository = studentsRepository;
            ClassesRepository = classesRepository;
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
                string errorMessage = $"Error saving changes to database: {exception.Message}\n\n{exception.InnerException}\n\n{exception.StackTrace}\n\n";
                Console.WriteLine(errorMessage);
            }
        }
    }
}
