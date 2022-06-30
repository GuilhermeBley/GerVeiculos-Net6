using ClienteNet6.Server.Identity;
using ClienteNet6.Server.Services;
using ClienteNet6.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace ClienteNet6.Server.Controllers
{
    /// <summary>
    /// Gerencia sessao de login
    /// </summary>
    [ApiController, Route("api/[controller]"), Produces("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }        

        [HttpGet, Authorize]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult<UserTokenJwt>> Post([FromBody] LoginUser userDto)
        {
           
            var user = await _userManager.FindByEmailAsync(userDto.UserName);

            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userDto.Password, false);

            if (result.IsNotAllowed)
            {
                return Unauthorized();
            }

            if (result.Succeeded)
            {
                return Ok(_tokenService.GetToken(user));
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
