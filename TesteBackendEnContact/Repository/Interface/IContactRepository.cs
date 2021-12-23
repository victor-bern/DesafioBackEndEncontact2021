using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Repository.Interface
{
    public interface IContactRepository : IRepository<IContact>
    {
        Task<ResultViewModel<IContact>> GetByParamAsync(string param, string value);
        Task<ResultViewModel<List<IContact>>> UploadContactsByFile(IFormFile file);
    }
}
