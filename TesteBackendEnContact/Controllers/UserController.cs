using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.User;
using TesteBackendEnContact.Extensions;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Controllers
{
    [Controller]
    [Route("users")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Authorize]

        public async Task<ResultViewModel<IEnumerable<User>>> Get() => await _userRepository.GetAllAsync();

        [HttpGet("{id}")]

        public async Task<ResultViewModel<User>> Get(int id) => await _userRepository.GetAsync(id);

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User model)
        {
            if (!ModelState.IsValid) return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));

            var user = await _userRepository.SaveAsync(model);

            if (user.Errors.Count > 0) return BadRequest(user);

            return Ok(model);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromBody] User model)
        {
            var claims = User.Claims;
            if (!ModelState.IsValid) return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));


            var user = await _userRepository.UpdateAsync(id, model);
            return Ok(user.Data);
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ResultViewModel<User>> Delete(int id) => await _userRepository.DeleteAsync(id);
    }
}
