using ClienteNet6.Server.Services;
using ClienteNet6.Server.Services.Exceptions;
using ClienteNet6.Shared.Dto;
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
        public async Task<IActionResult> GetVeiculo([FromQuery] int renavam)
        {
            try
            {
                var result = await _veiculoService.GetVeiculo(renavam);

                if (result is not null)
                    return Ok(result);
                return NoContent();
            }
            catch
            {
                return BadRequest("Erro ao coletar veículo");
            }
        }

        [HttpGet("all"), Authorize]
        public async Task<IActionResult> GetAllVeiculo()
        {
            try
            {
                var result = await _veiculoService.GetVeiculo();

                if (result.Any())
                    return Ok(result);
                return NoContent();
            }
            catch
            {
                return BadRequest("Erro ao coletar veículos");
            }
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> PostVeiculo([FromBody] VeiculoDto dto)
        {
            try
            {
                await _veiculoService.AddVeiculo(dto);

                return CreatedAtAction(nameof(GetVeiculo), dto);
            }
            catch (ConflictPostException)
            {
                return Conflict(dto);
            }
            catch
            {
                return BadRequest("Erro ao inserir veículo.");
            }
        }

        [HttpPut("{renavam:int}"), Authorize]
        public async Task<IActionResult> PutVeiculo(int renavam, VeiculoDto veiculo)
        {
            try
            {
                await _veiculoService.UpdateVeiculo(renavam, veiculo);

                return Ok();
            }
            catch (NoContentException)
            {
                return NoContent();
            }
            catch
            {
                return BadRequest("Erro ao inserir veículo.");
            }
        }
        
        [HttpDelete("{renavam:int}"), Authorize]
        public async Task<IActionResult> DeleteVeiculo(int renavam)
        {
            try
            {
                await _veiculoService.DeleteVeiculo(renavam);

                return Ok();
            }
            catch (NoContentException)
            {
                return NoContent();
            }
            catch
            {
                return BadRequest("Erro ao deletar veículo.");
            }
        }
    }

    
}
