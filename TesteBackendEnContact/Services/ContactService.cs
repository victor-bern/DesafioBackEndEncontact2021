using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.Core.Domain.ContactBook.Contact;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.Services.Interface;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Services
{
    public class ContactService : IContactService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IContactBookRepository _contactBookRepository;
        public ContactService(ICompanyRepository companyRepository, IContactBookRepository contactBookRepository)
        {
            _companyRepository = companyRepository;
            _contactBookRepository = contactBookRepository;
        }

        public async Task<IEnumerable<Contact>> ExtractContacts(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            var list = new List<Contact>();
            while (reader.Peek() != -1)
            {
                var data = reader.ReadLine().Split(",");
                if (data.Length < 5) continue;

                var name = data[0];
                var phone = data[1];
                var email = data[2];
                var address = data[3];
                var companyName = data[4];
                var contactBookName = data[5];

                ResultViewModel<ContactBook> contactBook;

                if (companyName.Length == 0)
                {
                    contactBook = await _contactBookRepository.GetByNameAsync(contactBookName);
                    if (contactBook.Data == null) continue;

                    list.Add(new Contact
                    {
                        Name = name,
                        Phone = phone,
                        Email = email,
                        Address = address,
                        ContactBookId = contactBook.Data.Id,
                        CompanyId = null
                    });
                    continue;
                }

                var company = await _companyRepository.GetByNameAsync(companyName);
                contactBook = await _contactBookRepository.GetByNameAsync(contactBookName);
                if (contactBook.Data == null || company.Data == null) continue;
                list.Add(new Contact
                {
                    Name = name,
                    Phone = phone,
                    Email = email,
                    Address = address,
                    ContactBookId = contactBook.Data.Id,
                    CompanyId = company.Data.Id,
                });

            }
            return list;
        }

    }
}
