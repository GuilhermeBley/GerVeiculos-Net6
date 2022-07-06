using ClienteNet6.Server.Context;
using ClienteNet6.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClienteNet6.Server.Controllers
{
    [ApiController, Route("api/[controller]"), Produces("application/json")]
    public class VeiculoController : ControllerBase
    {
        private readonly AppGerVeiculosContext _context;
        private readonly IUserService _userInfo;

        public VeiculoController(AppGerVeiculosContext context, IUserService userInfo)
        {
            _context = context;
            _userInfo = userInfo;
        }

        [HttpGet, Authorize]
        public IActionResult Get()
        {
            return Ok();
        }
    }

    
}
