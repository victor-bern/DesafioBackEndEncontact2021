using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook.Company;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("companies")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly IRepository<ICompany> _companyRepository;

        public CompanyController(ILogger<CompanyController> logger, IRepository<ICompany> companyRepository)
        {
            _logger = logger;
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<ResultViewModel<IEnumerable<ICompany>>> Get() => await _companyRepository.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ResultViewModel<ICompany>> Get(int id) => await _companyRepository.GetAsync(id);

        [HttpPost]
        public async Task<ResultViewModel<ICompany>> Post([FromBody] Company company) => await _companyRepository.SaveAsync(company);

        [HttpPut("{id}")]
        public async Task<ResultViewModel<ICompany>> Update(int id, [FromBody] Company model) => await _companyRepository.UpdateAsync(id, model);

        [HttpDelete("{id}")]
        public async Task<ResultViewModel<ICompany>> Delete(int id) => await _companyRepository.DeleteAsync(id);
    }
}
