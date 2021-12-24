using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TesteBackendEnContact.Core.Domain.ContactBook.Company;
using TesteBackendEnContact.Core.Domain.ContactBook.Contact;

namespace TesteBackendEnContact.ViewModels
{
    [Table("Company")]
    public class CompanyListViewModel : Company
    {
        public List<Contact> Contacts { get; set; } = new();
    }
}
