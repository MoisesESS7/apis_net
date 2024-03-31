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
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var veiculo = await _veiculoRepository.GetByIdAsync(id);

            if (veiculo is null)
            {
                return NotFound("Veículo não encontado.");
            }

            return Ok(veiculo);
        }

        [HttpGet("veiculos")]
        public async Task<IActionResult> GetAllAsync()
        {
            var veiculos = await _veiculoRepository.GetAllAsync();

            if (veiculos.Count == 0)
            {
                return NotFound("Nenhum veículo foi encontrado.");
            }

            return Ok(veiculos);
        }

        [HttpPost("veiculos")]
        public async Task<IActionResult> CreateAsync([FromBody] Veiculo veiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Estado do modelo inválido.");
            }

            await _veiculoRepository.CreateAsync(veiculo);

            return Ok(veiculo);
        }

        [HttpPut("veiculos/{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] Veiculo veiculoRequisicao, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Estado do modelo inválido.");
            }

            if (veiculoRequisicao.Id != id)
            {
                return BadRequest("Os id's da requisição e da url não condizem.");
            }

            var veiculoDb = await _veiculoRepository.GetByIdAsync(id);

            if (veiculoDb is null)
            {
                return NotFound("Veículo não encontado.");
            }

            await _veiculoRepository.UpdateAsync(veiculoDb, veiculoRequisicao);

            return Ok(veiculoRequisicao);
        }

        [HttpDelete("veiculos/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var veiculo = await _veiculoRepository.GetByIdAsync(id);

            if (veiculo is null)
            {
                return NotFound("Veículo não encontado.");
            }

            await _veiculoRepository.DeleteAsync(veiculo);

            return Ok(veiculo);
        }
    }
}
