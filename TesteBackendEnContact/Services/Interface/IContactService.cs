using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TesteBackendEnContact.Services.Interface
{
    public interface IContactService
    {
        Task<bool> ExtractContacts(IFormFile file);
    }
}
