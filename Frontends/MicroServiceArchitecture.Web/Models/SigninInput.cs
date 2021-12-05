using System.ComponentModel.DataAnnotations;

namespace MicroServiceArchitecture.Web.Models
{
    public class SigninInput
    {
        [Display(Name ="Email adresiniz")]
        public string Email { get; set; }

        [Display(Name = "Sifreniz")]
        public string Password { get; set; }

        [Display(Name = "Beni hatirla")]
        public bool IsRemember { get; set; }
    }
}
