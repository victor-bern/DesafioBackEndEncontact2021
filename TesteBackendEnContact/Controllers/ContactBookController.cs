using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.Core.Interface.ContactBook;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IContactBook> Post(ContactBook contactBook) => await _contactBookRepository.SaveAsync(contactBook);


        [HttpDelete]
        public async Task Delete(int id) => await _contactBookRepository.DeleteAsync(id);


        [HttpGet]
        public async Task<IEnumerable<IContactBook>> Get() => await _contactBookRepository.GetAllAsync();


        [HttpGet("{id}")]
        public async Task<IContactBook> Get(int id) => await _contactBookRepository.GetAsync(id);

    }
}
