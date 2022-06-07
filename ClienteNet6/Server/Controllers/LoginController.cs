using ClienteNet6.Server.Identity;
using ClienteNet6.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace ClienteNet6.Server.Controllers
{
    /// <summary>
    /// Gerencia sessao de login
    /// </summary>
    [ApiController, AllowAnonymous, Route("api/[controller]"), Produces("application/json")]
    public class LoginController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }        

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginUser userDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userDto.UserName);

                if (user == null)
                {
                    return Unauthorized();
                }

                var result = await _signInManager.PasswordSignInAsync(user, userDto.Password, false, false);

                if (result.IsNotAllowed)
                {
                    return Unauthorized();
                }

                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }

            return BadRequest("Modelo inválido");
        }
    }
}
