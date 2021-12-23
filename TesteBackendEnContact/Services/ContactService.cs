using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook.Contact;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.Services.Interface;

namespace TesteBackendEnContact.Services
{
    public class ContactService : IContactService
    {
        private readonly IRepository<ICompany> _companyRepository;
        public ContactService(IRepository<ICompany> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<bool> ExtractContacts(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            var list = new List<Contact>();
            while (reader.Peek() != -1)
            {
                var data = reader.ReadLine().Split(",");
                if (data.Length < 4) continue;

                var name = data[0];
                var phone = data[1];
                var email = data[2];
                var address = data[3];
                var companyName = data[4];
                var contactBookId = data[5];



            }
            return true;
        }

    }
}
