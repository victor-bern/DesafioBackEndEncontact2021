namespace TesteBackendEnContact.Core.Interface.ContactBook.Contact
{
    public interface IContact
    {
        int Id { get; set; }
        string Name { get; set; }
        string Phone { get; set; }
        string Email { get; set; }
        string Address { get; set; }
        int CompanyId { get; set; }
        int ContactBookId { get; set; }
    }
}
