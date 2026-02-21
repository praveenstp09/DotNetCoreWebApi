using System.ComponentModel.DataAnnotations;

namespace FirstWebAPI.DTO
{
    public class RegisterUserDTO
    {
        [Required]    
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string Confirm { get; set; } = string.Empty;

        [Required]
        public int? Age { get; set; }

        [Required]
        public List<int>? RoleIds { get; set; }
    }
}
