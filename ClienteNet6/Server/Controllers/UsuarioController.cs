using ClienteNet6.Server.Identity;
using ClienteNet6.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace ClienteNet6.Server.Controllers
{
    /// <summary>
    /// Lida com CRUD de usuarios
    /// </summary>
    [ApiController, AllowAnonymous, Route("api/[controller]"), Produces("application/json")]
    public class UsuarioController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UsuarioController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginCreate userDto)
        {
            if (string.IsNullOrEmpty(userDto.Email))
                return BadRequest(nameof(userDto.Email));

            if (ModelState.IsValid)
            {
                if (await _userManager.FindByNameAsync(userDto.Email) is not null)
                {
                    return BadRequest("Usuário já existente.");
                }

                var user = new User { Email = userDto.Email, UserName = userDto.Email, NormalizedUserName = userDto.Email, NomeEmpresa = userDto.NomeEmpresa };

                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(System.Text.Encoding.UTF8.GetBytes(code));

                    return Ok();
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }

            return BadRequest("Modelo inválido");
        }
    }
}
