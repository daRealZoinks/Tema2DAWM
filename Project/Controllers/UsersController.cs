using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto registerData)
        {
            var result = _userService.Register(registerData);

            if (result == null)
            {
                return BadRequest("User cannot be registered");
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto payload)
        {
            var result = _userService.Validate(payload);

            if (result == null)
            {
                return BadRequest("Invalid credentials");
            }

            return Ok(result);
        }
    }
}
