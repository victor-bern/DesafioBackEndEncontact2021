using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook.Contact;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Repository.Interface
{
    public interface IContactRepository
    {
        Task<ResultViewModel<IEnumerable<Contact>>> GetAllAsync();
        Task<ResultViewModel<Contact>> GetAsync(int id);
        Task<ResultViewModel<Contact>> GetByNameAsync(string name);
        Task<ResultViewModel<Contact>> SaveAsync(Contact entity);
        Task<ResultViewModel<Contact>> UpdateAsync(int id, Contact entity);
        Task<ResultViewModel<Contact>> DeleteAsync(int id);
        Task<ResultViewModel<Contact>> GetByParamAsync(string param, string value);
        Task<ResultViewModel<IEnumerable<Contact>>> UploadContactsByFile(IFormFile file);

    }
}
