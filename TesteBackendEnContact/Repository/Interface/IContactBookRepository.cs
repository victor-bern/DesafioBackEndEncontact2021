using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Repository.Interface
{
    public interface IContactBookRepository
    {
        Task<ResultViewModel<IEnumerable<ContactBook>>> GetAllAsync();
        Task<ResultViewModel<ContactBook>> GetAsync(int id);
        Task<ResultViewModel<ContactBook>> GetByNameAsync(string name);
        Task<ResultViewModel<ContactBook>> SaveAsync(ContactBook entity);
        Task<ResultViewModel<ContactBook>> UpdateAsync(int id, ContactBook entity);
        Task<ResultViewModel<ContactBook>> DeleteAsync(int id);
        Task TruncateTables();

    }
}
