using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Repository.Interface
{
    public interface IRepository<T> where T : class
    {

        Task<ResultViewModel<IEnumerable<T>>> GetAllAsync();
        Task<ResultViewModel<T>> GetAsync(int id);
        Task<ResultViewModel<T>> GetByNameAsync(string name);
        Task<ResultViewModel<T>> SaveAsync(T entity);
        Task<ResultViewModel<T>> UpdateAsync(int id, T entity);
        Task<ResultViewModel<T>> DeleteAsync(int id);
    }
}
