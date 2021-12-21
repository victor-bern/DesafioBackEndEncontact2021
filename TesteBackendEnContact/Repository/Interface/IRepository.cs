using System.Collections.Generic;
using System.Threading.Tasks;

namespace TesteBackendEnContact.Repository.Interface
{
    public interface IRepository<T>
    {
        Task<T> SaveAsync(T company);
        Task DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
    }
}
