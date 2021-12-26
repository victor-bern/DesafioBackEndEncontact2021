using System.ComponentModel.DataAnnotations;

namespace TesteBackendEnContact.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email e senha requeridos")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Email e senha requeridos")]
        public string Password { get; set; }
    }

}
