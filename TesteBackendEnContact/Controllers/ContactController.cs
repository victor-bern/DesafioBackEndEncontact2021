using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Controllers.Models;
using TesteBackendEnContact.Core.Domain.ContactBook.Contact;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("contacts")]
    [Authorize]

    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactRepository _contactRepository;

        public ContactController(ILogger<ContactController> logger, IContactRepository contactRepository)
        {
            _logger = logger;
            _contactRepository = contactRepository;
        }

        [HttpGet("search-by-param")]
        public async Task<ResultViewModel<Contact>> GeyByParam([FromQuery] string param, [FromQuery] string value) => await _contactRepository.GetByParamAsync(param, value);

        [HttpGet]
        public async Task<ResultViewModel<IEnumerable<Contact>>> Get() => await _contactRepository.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ResultViewModel<Contact>> GetById(int id) => await _contactRepository.GetAsync(id);


        [HttpPost]
        public async Task<ResultViewModel<Contact>> Save([FromBody] Contact model) => await _contactRepository.SaveAsync(model);

        [HttpPost("send-contacts")]
        public async Task<IActionResult> UploadFile([FromForm] ContactFormFile model) => Ok(await _contactRepository.UploadContactsByFile(model.file));

        [HttpPut("{id}")]
        public async Task<ResultViewModel<Contact>> Update(int id, [FromBody] Contact model) => await _contactRepository.UpdateAsync(id, model);

        [HttpDelete("{id}")]
        public async Task<ResultViewModel<Contact>> Delete(int id) => await _contactRepository.DeleteAsync(id);

    }
}
