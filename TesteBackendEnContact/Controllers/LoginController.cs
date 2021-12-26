using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TesteBackendEnContact.Extensions;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.Services.Interface;
using TesteBackendEnContact.ViewModels;

namespace TesteBackendEnContact.Controllers
{
    [Controller]
    [Route("login")]
    public class LoginController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var user = await _userRepository.GetByEmailAsync(model.Email);

            if (user.Data == null || user.Data.Password != model.Password) return BadRequest(new ResultViewModel<string>("Email ou senha inválidos"));

            var token = _tokenService.CreateToken(user.Data);


            return Ok(new ResultViewModel<string>(data: token));
        }
    }
}
