using Core.Dtos;
using DataLayer;
using DataLayer.Entities;
using DataLayer.Enums;

namespace Core.Services
{
    public class UserService
    {
        private readonly UnitOfWork _unitOfWork;

        private readonly AuthorizationService _authorizationService;

        public UserService(UnitOfWork unitOfWork, AuthorizationService authorizationService)
        {
            _unitOfWork = unitOfWork;
            _authorizationService = authorizationService;
        }

        public RegisterDto Register(RegisterDto payload)
        {
            if (payload == null)
            {
                return null;
            }

            var hashedPassword = _authorizationService.HashPassword(payload.Password);

            if (hashedPassword == null)
            {
                return null;
            }

            User newUser = new()
            {
                Email = payload.Email,
                PasswordHash = hashedPassword,
                Role = payload.Role,
            };

            _unitOfWork.UsersRepository.Insert(newUser);
            _unitOfWork.SaveChanges();

            return payload;
        }

        public string Validate(LoginDto payload)
        {
            var user = _unitOfWork.UsersRepository.GetByEmail(payload.Email);

            var isPasswordFine = _authorizationService.VerifyHashedPassword(user.PasswordHash, payload.Password);

            if (isPasswordFine)
            {
                var role = GetRole(user);

                return _authorizationService.GetToken(user, role);
            }
            else
            {
                return null;
            }
        }

        public Role GetRole(User user)
        {
            return user.Email == "murarasuvlad@gmail.com" ? Role.Teacher : Role.Student;
        }
    }
}
