using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("contactbooks")]
    public class ContactBookController : ControllerBase
    {
        private readonly ILogger<ContactBookController> _logger;
        private readonly IContactBookRepository _contactBookRepository;

        public ContactBookController(ILogger<ContactBookController> logger, IContactBookRepository contactBookRepository)
        {
            _logger = logger;
            _contactBookRepository = contactBookRepository;
        }


        [HttpGet]
        public async Task<ResultViewModel<IEnumerable<ContactBook>>> Get() => await _contactBookRepository.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ResultViewModel<ContactBook>> Get(int id) => await _contactBookRepository.GetAsync(id);

        [HttpPost]
        public async Task<ResultViewModel<ContactBook>> Post(ContactBook contactBook) => await _contactBookRepository.SaveAsync(contactBook);

        [HttpPut("{id}")]
        public async Task<ResultViewModel<ContactBook>> Update(int id, [FromBody] ContactBook model) => await _contactBookRepository.UpdateAsync(id, model);

        [HttpDelete("{id}")]
        public async Task<ResultViewModel<ContactBook>> Delete(int id) => await _contactBookRepository.DeleteAsync(id);

        [HttpGet("truncate")]
        public async Task<IActionResult> Truncate()
        {
            await _contactBookRepository.TruncateTables();
            return Ok();
        }

    }
}
