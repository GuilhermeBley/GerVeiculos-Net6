using ClienteNet6.Server.Identity;
using ClienteNet6.Server.Services;
using ClienteNet6.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace ClienteNet6.Server.Controllers
{
    [ApiController, Route("api/[controller]"), Produces("application/json")]
    public class VeiculoController : ControllerBase
    {
        public VeiculoController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }

    
}
