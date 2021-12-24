using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook.Company;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("companies")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ILogger<CompanyController> logger, ICompanyRepository companyRepository)
        {
            _logger = logger;
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<ResultViewModel<IEnumerable<CompanyListViewModel>>> Get() => await _companyRepository.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ResultViewModel<Company>> Get(int id) => await _companyRepository.GetAsync(id);

        [HttpPost]
        public async Task<ResultViewModel<Company>> Post([FromBody] Company company) => await _companyRepository.SaveAsync(company);

        [HttpPut("{id}")]
        public async Task<ResultViewModel<Company>> Update(int id, [FromBody] Company model) => await _companyRepository.UpdateAsync(id, model);

        [HttpDelete("{id}")]
        public async Task<ResultViewModel<Company>> Delete(int id) => await _companyRepository.DeleteAsync(id);
    }
}
