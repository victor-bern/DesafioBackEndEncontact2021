using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook.Contact;

namespace TesteBackendEnContact.Services.Interface
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> ExtractContacts(IFormFile file);
    }
}
