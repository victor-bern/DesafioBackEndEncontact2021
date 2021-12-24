using TesteBackendEnContact.Core.Domain.User;

namespace TesteBackendEnContact.Services.Interface
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
