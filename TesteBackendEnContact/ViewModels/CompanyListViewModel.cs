using System.Collections.Generic;
using TesteBackendEnContact.Core.Domain.ContactBook.Company;
using TesteBackendEnContact.Core.Domain.ContactBook.Contact;

namespace TesteBackendEnContact.ViewModels
{
    public class CompanyListViewModel : Company
    {
        public List<Contact>? Contacts { get; set; } = new();
    }
}
