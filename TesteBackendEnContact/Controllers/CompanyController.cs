using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Controllers.Models;
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

        [HttpPost]
        public async Task<ActionResult<ICompany>> Post(SaveCompanyRequest company)
        {
            return Ok(await _companyRepository.SaveAsync(company.ToCompany()));
        }

        [HttpDelete("{id}")]
        public async Task<ResultViewModel<ICompany>> Delete(int id)
        {
            return await _companyRepository.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<ResultViewModel<IEnumerable<ICompany>>> Get()
        {
            return await _companyRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ResultViewModel<ICompany>> Get(int id)
        {
            return await _companyRepository.GetAsync(id);
        }
    }
}
