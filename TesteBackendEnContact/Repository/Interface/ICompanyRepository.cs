using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook.Company;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Repository.Interface
{
    public interface ICompanyRepository
    {
        Task<ResultViewModel<IEnumerable<CompanyWithContactListViewModel>>> GetAllAsync();
        Task<ResultViewModel<Company>> GetAsync(int id);
        Task<ResultViewModel<CompanyWithContactListViewModel>> GetContactsInCompanyByName(string companyName, int contactBookId);
        Task<ResultViewModel<Company>> GetByNameAsync(string name);
        Task<ResultViewModel<Company>> SaveAsync(Company entity);
        Task<ResultViewModel<Company>> UpdateAsync(int id, Company entity);
        Task<ResultViewModel<Company>> DeleteAsync(int id);

    }
}
