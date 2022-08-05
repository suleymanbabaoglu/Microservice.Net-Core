using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class LoginInput
    {
        [Required, Display(Name = "Email Adresiniz")]
        public string Email { get; set; }
        [Required, Display(Name = "Parolanız")]
        public string Password { get; set; }
        [Display(Name = "Beni Hatırla")]
        public bool IsRemember { get; set; }

    }
}
