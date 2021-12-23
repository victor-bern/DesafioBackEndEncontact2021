using System.ComponentModel.DataAnnotations.Schema;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;

namespace TesteBackendEnContact.Core.Domain.ContactBook.Company
{
    [Table("Company")]
    public class Company : ICompany
    {
        public int Id { get; private set; }
        public int ContactBookId { get; private set; }
        public string Name { get; set; }


        public Company()
        {

        }

        public Company(ICompany company)
        {
            Id = company.Id;
            ContactBookId = company.ContactBookId;
            Name = company.Name;
        }
    }
}
