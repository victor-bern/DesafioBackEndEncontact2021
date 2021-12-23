using System.ComponentModel.DataAnnotations.Schema;
using TesteBackendEnContact.Core.Interface.ContactBook;

namespace TesteBackendEnContact.Core.Domain.ContactBook
{
    [Table("ContactBook")]
    public class ContactBook : IContactBook
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ContactBook()
        {

        }

        public ContactBook(IContactBook contactBook)
        {
            Id = contactBook.Id;
            Name = contactBook.Name;
        }
    }
}
