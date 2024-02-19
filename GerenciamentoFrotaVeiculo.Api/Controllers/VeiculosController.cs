using GerenciamentoFrotaVeiculo.Models;
using GerenciamentoFrotaVeiculo.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoFrotaVeiculo.Controllers
{
    [Route("api/")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculosController(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        [HttpGet("veiculos/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var veiculo = await _veiculoRepository.GetAsync(id);

            return Ok(veiculo);
        }

        [HttpGet("veiculos/")]
        public async Task<IActionResult> GetAllAsync()
        {
            var veiculos = await _veiculoRepository.GetAllAsync();

            return Ok(veiculos);
        }

        [HttpPost("veiculos")]
        public async Task<IActionResult> CreateAsync([FromBody] Veiculo veiculo)
        {
            await _veiculoRepository.CreateAsync(veiculo);

            return Ok(veiculo);
        }

        [HttpPut("veiculos/{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] Veiculo veiculo)
        {
            await _veiculoRepository.UpdateAsync(veiculo);

            return Ok(veiculo);
        }

        [HttpDelete("veiculos/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var veiculo = await _veiculoRepository.GetAsync(id);
            await _veiculoRepository.DeleteAsync(veiculo);

            return Ok(veiculo);
        }
    }
}
