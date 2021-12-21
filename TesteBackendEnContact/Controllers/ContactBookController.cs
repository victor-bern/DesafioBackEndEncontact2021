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

        public ContactBookController(ILogger<ContactBookController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IContactBook> Post(ContactBook contactBook, [FromServices] IContactBookRepository contactBookRepository) => await contactBookRepository.SaveAsync(contactBook);


        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromServices] IContactBookRepository contactBookRepository)
        {
            var isDeleted = await contactBookRepository.DeleteAsync(id);
            if (!isDeleted) return StatusCode(204);
            return Ok();

        }

        [HttpGet]
        public async Task<IEnumerable<IContactBook>> Get([FromServices] IContactBookRepository contactBookRepository) => await contactBookRepository.GetAllAsync();


        [HttpGet("{id}")]
        public async Task<IContactBook> Get(int id, [FromServices] IContactBookRepository contactBookRepository) => await contactBookRepository.GetAsync(id);

    }
}
