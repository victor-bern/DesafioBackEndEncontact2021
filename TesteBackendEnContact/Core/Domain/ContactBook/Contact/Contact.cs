using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;

namespace TesteBackendEnContact.Core.Domain.ContactBook.Contact
{
    [Table("Contact")]
    public class Contact : IContact
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int CompanyId { get; set; }
        public int ContactBookId { get; set; }


        public Contact()
        {

        }
        public Contact(IContact contact)
        {
            Name = contact.Name;
            Phone = contact.Phone;
            Email = contact.Email;
            Address = contact.Address;
            CompanyId = contact.CompanyId;
            ContactBookId = contact.ContactBookId;
        }
    }
}
