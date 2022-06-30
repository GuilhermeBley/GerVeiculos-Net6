using ClienteNet6.Server.Identity;
using ClienteNet6.Server.Services;
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
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public UsuarioController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<UserTokenJwt>> Post([FromBody] LoginCreate userDto)
        {
            if (string.IsNullOrEmpty(userDto.Email))
                return BadRequest(nameof(userDto.Email));

            
            if (await _userManager.FindByEmailAsync(userDto.Email) is not null)
            {
                return BadRequest("Usuário já existente.");
            }

            var user = new User { Email = userDto.Email, UserName = userDto.Nome, NormalizedUserName = userDto.Email, NomeEmpresa = userDto.NomeEmpresa };

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                user = await _userManager.FindByEmailAsync(userDto.Email);

                return Ok(_tokenService.GetToken(user));
            }
            else
            {
                return BadRequest("Senha requer letra maiúscula, minúscula, caracteres numéricos e especiais.");
            }
        }
    }
}
