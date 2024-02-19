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
        public async Task<IActionResult> GetAsync(int id)
        {
            var colaborador = await _colaboradorRepository.GetAsync(id);

            return Ok(colaborador);
        }

        [HttpGet("colaboradores")]
        public async Task<IActionResult> GetAllAsync()
        {
            var colaboradores = await _colaboradorRepository.GetAllAsync();

            return Ok(colaboradores);
        }

        [HttpPost("colaboradores")]
        public async Task<IActionResult> CreateAsync([FromBody] Colaborador colaborador)
        {
            await _colaboradorRepository.CreateAsync(colaborador);

            return Ok(colaborador);
        }

        [HttpPut("colaboradores/{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] Colaborador colaboradorRequisicao, int id)
        {
            var colaboradorDb = await _colaboradorRepository.GetAsync(id);
            await _colaboradorRepository.UpdateAsync(colaboradorRequisicao, colaboradorDb);

            return Ok(colaboradorRequisicao);
        }

        [HttpDelete("colaboradores/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var colaborador = await _colaboradorRepository.GetAsync(id);
            await _colaboradorRepository.DeleteAsync(colaborador);
            
            return Ok(colaborador);
        }
    }
}
