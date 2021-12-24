using System.ComponentModel.DataAnnotations.Schema;

namespace TesteBackendEnContact.Core.Domain.ContactBook.Contact
{
    [Table("Contact")]
    public class Contact : Base
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int? CompanyId { get; set; }
        public int ContactBookId { get; set; }


        public Contact()
        {

        }
        public Contact(Contact contact)
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
