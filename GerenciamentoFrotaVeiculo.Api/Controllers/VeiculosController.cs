using GerenciamentoFrotaVeiculo.Models;
using GerenciamentoFrotaVeiculo.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoFrotaVeiculo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculosController(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var veiculo = await _veiculoRepository.GetAsync(id);

            return Ok(veiculo);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var veiculos = await _veiculoRepository.GetAllAsync();

            return Ok(veiculos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Veiculo veiculo)
        {
            await _veiculoRepository.CreateAsync(veiculo);

            return Ok(veiculo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] Veiculo veiculo)
        {
            await _veiculoRepository.UpdateAsync(veiculo);

            return Ok(veiculo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var veiculo = await _veiculoRepository.GetAsync(id);
            await _veiculoRepository.DeleteAsync(veiculo);

            return Ok(veiculo);
        }
    }
}
