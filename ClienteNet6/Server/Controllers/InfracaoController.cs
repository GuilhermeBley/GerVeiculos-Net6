using ClienteNet6.Server.Services;
using ClienteNet6.Server.Services.Exceptions;
using ClienteNet6.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClienteNet6.Server.Controllers
{
    [ApiController, Route("api/[controller]"), Produces("application/json")]
    public class InfracaoController : ControllerBase
    {
        private readonly IInfracaoService _infracaoService;

        public InfracaoController(IInfracaoService infracaoService)
        {
            _infracaoService = infracaoService;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetInfracao([FromQuery]string ait)
        {
            try
            {
                var result = await _infracaoService.GetInfracao(ait);

                if (result is not null)
                    return Ok(result);
                return NoContent();
            }
            catch
            {
                return BadRequest("Erro ao coletar infração");
            }
        }

        [HttpGet("all"), Authorize]
        public async Task<IActionResult> GetInfracao()
        {
            try
            {
                var result = await _infracaoService.GetInfracao();

                if (result.Any())
                    return Ok(result);
                return NoContent();
            }
            catch
            {
                return BadRequest("Erro ao coletar infrações");
            }
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> PostInfracao([FromBody] InfracaoDto dto)
        {
            try
            {
                await _infracaoService.AddInfracao(dto);

                return CreatedAtAction(nameof(GetInfracao), dto);
            }
            catch (ConflictPostException)
            {
                return Conflict(dto);
            }
            catch
            {
                return BadRequest("Erro ao inserir infração.");
            }
        }

        [HttpPut("{ait}"), Authorize]
        public async Task<IActionResult> PutInfracao(string ait, InfracaoDto infracao)
        {
            try
            {
                await _infracaoService.UpdateInfracao(ait, infracao);

                return Ok();
            }
            catch (NoContentException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return BadRequest("Erro ao atualizar infração.");
            }
        }

        [HttpDelete("{ait}"), Authorize]
        public async Task<IActionResult> DeleteInfracao(string ait)
        {
            try
            {
                await _infracaoService.DeleteInfracao(ait);

                return Ok();
            }
            catch (NoContentException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return BadRequest("Erro ao deletar infração.");
            }
        }
    }
}
