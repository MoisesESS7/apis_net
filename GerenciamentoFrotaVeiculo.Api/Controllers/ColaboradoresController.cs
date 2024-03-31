using GerenciamentoFrotaVeiculo.Models;
using GerenciamentoFrotaVeiculo.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoFrotaVeiculo.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ColaboradoresController : ControllerBase
    {
        private readonly IColaboradorRepository _colaboradorRepository;

        public ColaboradoresController(IColaboradorRepository colaboradorRepository)
        {
            _colaboradorRepository = colaboradorRepository;
        }

        [HttpGet("colaboradores/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var colaborador = await _colaboradorRepository.GetByIdAsync(id);

            if(colaborador is null)
            {
                return NotFound("Colaborador não encontrado.");
            }

            return Ok(colaborador);
        }

        [HttpGet("colaboradores")]
        public async Task<IActionResult> GetAllAsync()
        {
            var colaboradores = await _colaboradorRepository.GetAllAsync();

            if(colaboradores.Count == 0)
            {
                return NotFound("Nenhum colaborador foi encontrado.");
            }

            return Ok(colaboradores);
        }

        [HttpPost("colaboradores")]
        public async Task<IActionResult> CreateAsync([FromBody] Colaborador colaborador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Estado do modelo inválido.");
            }

            await _colaboradorRepository.CreateAsync(colaborador);

            return Ok(colaborador);
        }

        [HttpPut("colaboradores/{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] Colaborador colaboradorRequisicao, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Estado do modelo inválido.");
            }

            if(colaboradorRequisicao.Id != id)
            {
                return BadRequest("Os id's da requisição e da url não condizem.");
            }

            var colaboradorDb = await _colaboradorRepository.GetByIdAsync(id);

            if(colaboradorDb is null)
            {
                return NotFound("Colaborador não encontrado.");
            }

            await _colaboradorRepository.UpdateAsync(colaboradorRequisicao, colaboradorDb);

            return Ok(colaboradorRequisicao);
        }

        [HttpDelete("colaboradores/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var colaborador = await _colaboradorRepository.GetByIdAsync(id);

            if (colaborador is null)
            {
                return NotFound("Colaborador não encontrado.");
            }

            await _colaboradorRepository.DeleteAsync(colaborador);
            
            return Ok(colaborador);
        }
    }
}
