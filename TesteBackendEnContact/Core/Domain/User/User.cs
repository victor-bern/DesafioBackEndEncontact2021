using System.ComponentModel.DataAnnotations.Schema;

namespace TesteBackendEnContact.Core.Domain.User
{
    [Table("User")]
    public class User : Base
    {
        public string Email { get; set; }
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
