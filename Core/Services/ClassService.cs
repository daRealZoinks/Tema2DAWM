using Core.Dtos;
using DataLayer;
using DataLayer.Entities;

namespace Core.Services
{
    public class ClassService
    {
        private readonly UnitOfWork _unitOfWork;

        public ClassService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ClassAddDto Add(ClassAddDto payload)
        {
            if (payload == null)
            {
                return null;
            }

            var hasNameConflict = _unitOfWork.ClassesRepository.Any(x => x.Name == payload.Name);

            if (hasNameConflict)
            {
                return null;
            }

            Class newClass = new()
            {
                Name = payload.Name,
            };

            _unitOfWork.ClassesRepository.Insert(newClass);
            _unitOfWork.SaveChanges();

            return payload;
        }

        public List<ClassViewDto> GetAll()
        {
            var classes = _unitOfWork.ClassesRepository.GetAll();

            return classes.Select(x => new ClassViewDto
            {
                Id = x.Id,
                Name = x.Name,
                StudentCount = x.StudentCount,
            }).ToList();
        }
    }
}
