using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteBackendEnContact.Core.Domain.User
{
    [Table("User")]
    public class User : Base
    {

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Password { get; set; }

        public User()
        {

        }

        public User(User user) : base(user.Id)
        {
            Email = user.Email;
            Password = user.Password;
        }
    }
}
