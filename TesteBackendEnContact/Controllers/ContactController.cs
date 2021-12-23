﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook.Contact;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("contacts")]
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
        public async Task<ResultViewModel<IContact>> GeyByParam([FromQuery] string param, [FromQuery] string value) => await _contactRepository.GetByParamAsync(param, value);

        [HttpGet]
        public async Task<ResultViewModel<IEnumerable<IContact>>> Get() => await _contactRepository.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ResultViewModel<IContact>> GetById(int id) => await _contactRepository.GetAsync(id);


        [HttpPost]
        public async Task<ResultViewModel<IContact>> Save([FromBody] Contact model) => await _contactRepository.SaveAsync(model);

        [HttpPut("{id}")]
        public async Task<ResultViewModel<IContact>> Update(int id, [FromBody] Contact model) => await _contactRepository.UpdateAsync(id, model);

        [HttpDelete("{id}")]
        public async Task<ResultViewModel<IContact>> Delete(int id) => await _contactRepository.DeleteAsync(id);

    }
}
