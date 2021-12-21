using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.Core.Interface.ContactBook;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("contactbooks")]
    public class ContactBookController : ControllerBase
    {
        private readonly ILogger<ContactBookController> _logger;
        private readonly IRepository<IContactBook> _contactBookRepository;

        public ContactBookController(ILogger<ContactBookController> logger, IRepository<IContactBook> contactBookRepository)
        {
            _logger = logger;
            _contactBookRepository = contactBookRepository;
        }

        [HttpPost]
        public async Task<ResultViewModel<IContactBook>> Post(ContactBook contactBook) => await _contactBookRepository.SaveAsync(contactBook);


        [HttpDelete("{id}")]
        public async Task<ResultViewModel<IContactBook>> Delete(int id) => await _contactBookRepository.DeleteAsync(id);


        [HttpGet]
        public async Task<ResultViewModel<IEnumerable<IContactBook>>> Get() => await _contactBookRepository.GetAllAsync();


        [HttpGet("{id}")]
        public async Task<ResultViewModel<IContactBook>> Get(int id) => await _contactBookRepository.GetAsync(id);

    }
}
