using TesteBackendEnContact.Core.Interface.ContactBook.Contact;

namespace TesteBackendEnContact.Core.Domain.ContactBook.Contact
{
    public class Contact : IContact
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public string Phone { get; set; }
        public int CompanyId { get; set; }
        public int ContactBookId { get; set; }
    }
}
