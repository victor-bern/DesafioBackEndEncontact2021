using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.User;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ResultViewModel<IEnumerable<User>>> Get() => await _userRepository.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ResultViewModel<User>> Get(int id) => await _userRepository.GetAsync(id);

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User company)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ResultViewModel<User>> Update(int id, [FromBody] User model) => await _userRepository.UpdateAsync(id, model);

        [HttpDelete("{id}")]
        public async Task<ResultViewModel<User>> Delete(int id) => await _userRepository.DeleteAsync(id);
    }
}
