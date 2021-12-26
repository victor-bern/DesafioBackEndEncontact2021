using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.User;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Repository.Interface
{
    public interface IUserRepository
    {
        Task<ResultViewModel<IEnumerable<User>>> GetAllAsync();
        Task<ResultViewModel<User>> GetAsync(int id);
        Task<ResultViewModel<User>> GetByEmailAsync(string email);
        Task<ResultViewModel<User>> SaveAsync(User entity);
        Task<ResultViewModel<User>> UpdateAsync(int id, User entity);
        Task<ResultViewModel<User>> DeleteAsync(int id);
    }
}
