using GerenciamentoFrotaVeiculo.Models;
using GerenciamentoFrotaVeiculo.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoFrotaVeiculo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradoresVeiculosController : ControllerBase
    {
        private readonly IColaboradorVeiculoRepository _colaboradorVeiculoRepository;
        private readonly IColaboradorRepository _colaboradorRepository;
        private readonly IVeiculoRepository _veiculoRepository;

        public ColaboradoresVeiculosController(IColaboradorVeiculoRepository colaboradorVeiculoRepository, IColaboradorRepository colaboradorRepository, IVeiculoRepository veiculo)
        {
            _colaboradorVeiculoRepository = colaboradorVeiculoRepository;
            _colaboradorRepository = colaboradorRepository;
            _veiculoRepository = veiculo;
        }

        [HttpGet("{colaboradorId}/{veiculoId}")]
        public async Task<IActionResult> GetAsync(int colaboradorId, int veiculoId)
        {
            var colaboradorVeiculo = await _colaboradorVeiculoRepository
                .GetAsync(colaboradorId, veiculoId);

            return Ok(colaboradorVeiculo);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var colaboradoresVeiculos = await _colaboradorVeiculoRepository.GetAllAsync();

            return Ok(colaboradoresVeiculos);
        }

        [HttpPost("{colaboradorId},{veiculoId}")]
        public async Task<IActionResult> CreateAsync([FromBody] ColaboradorVeiculo colaboradorVeiculo, int colaboradorId, int veiculoId)
        {
            var colaborador = await _colaboradorRepository.GetAsync(colaboradorId);
            var veiculo = await _veiculoRepository.GetAsync(veiculoId);

            colaboradorVeiculo.Colaborador = colaborador;
            colaboradorVeiculo.Veiculo = veiculo;
            colaboradorVeiculo.DataInicioVinculo = DateTimeOffset.Now;

            await _colaboradorVeiculoRepository.CreateAsync(colaboradorVeiculo);

            return Ok(colaboradorVeiculo);
        }

        [HttpPut("{colaboradorId},{veiculoId}")]
        public async Task<IActionResult> UpdateAsync([FromBody] ColaboradorVeiculo colaboradorVeiculoRequisicao, int colaboradorId, int veiculoId)
        {
            var colaboradorVeiculoDb = 
                await _colaboradorVeiculoRepository.GetAsync(colaboradorId, veiculoId);

            await _colaboradorVeiculoRepository
                .UpdateAsync(colaboradorVeiculoRequisicao, colaboradorVeiculoDb);

            return Ok(colaboradorVeiculoRequisicao);
        }

        [HttpDelete("{colaboradorId},{veiculoId}")]
        public async Task<IActionResult> DeleteAsync(int colaboradorId, int veiculoId)
        {
            var colaboradorVeiculo = await _colaboradorVeiculoRepository.GetAsync(colaboradorId, veiculoId);
            await _colaboradorVeiculoRepository.DeleteAsync(colaboradorVeiculo);

            return Ok(colaboradorVeiculo);
        }
    }
}
