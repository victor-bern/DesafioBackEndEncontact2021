using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Repository.Interface
{
    public interface ICompanyRepository : IRepository<ICompany>
    {
        Task<ResultViewModel<ICompany>> GetCompanyByNameAsync(string name);
    }
}
