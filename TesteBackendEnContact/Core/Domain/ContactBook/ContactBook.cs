using System.ComponentModel.DataAnnotations.Schema;

namespace TesteBackendEnContact.Core.Domain.ContactBook
{
    [Table("ContactBook")]
    public class ContactBook : Base
    {
        public string Name { get; set; }

        public ContactBook()
        {

        }

        public ContactBook(ContactBook contactBook) : base(contactBook.Id)
        {
            Name = contactBook.Name;
        }
    }
}
