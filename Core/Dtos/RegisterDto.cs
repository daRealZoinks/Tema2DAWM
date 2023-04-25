using DataLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class RegisterDto
    {
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        [Required] public Role Role { get; set; }
    }
}
