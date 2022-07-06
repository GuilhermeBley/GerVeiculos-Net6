using ClienteNet6.Server.Context;
using ClienteNet6.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClienteNet6.Server.Controllers
{
    [ApiController, Route("api/[controller]"), Produces("application/json")]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoService _veiculoService;

        public VeiculoController(IVeiculoService veiculoService)
        {
            _veiculoService = veiculoService;
        }

        [HttpGet, Authorize]
        public IActionResult Get()
        {
            return Ok();
        }
    }

    
}
