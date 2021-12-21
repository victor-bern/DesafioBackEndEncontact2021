namespace TesteBackendEnContact.Core.Interface.ContactBook.Contact
{
    public interface IContact
    {
        int Id { get; set; }
        int Name { get; set; }
        string Phone { get; set; }
        int CompanyId { get; set; }
        int ContactBookId { get; set; }
    }
}
