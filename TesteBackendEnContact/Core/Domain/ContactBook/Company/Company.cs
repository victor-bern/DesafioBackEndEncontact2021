using System.ComponentModel.DataAnnotations.Schema;

namespace TesteBackendEnContact.Core.Domain.ContactBook.Company
{
    [Table("Company")]
    public class Company : Base
    {
        public int ContactBookId { get; set; }
        public string Name { get; set; }

        public Company()
        {

        }

        public Company(Company company) : base(company.Id)
        {
            ContactBookId = company.ContactBookId;
            Name = company.Name;
        }
    }
}
