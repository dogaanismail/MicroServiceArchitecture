using System.ComponentModel.DataAnnotations;

namespace MicroServiceArchitecture.IdentityServer.Dtos
{
    public class SignupDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
