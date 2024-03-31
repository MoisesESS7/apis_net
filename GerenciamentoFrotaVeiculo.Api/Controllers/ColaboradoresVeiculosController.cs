using GerenciamentoFrotaVeiculo.Models;
using GerenciamentoFrotaVeiculo.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoFrotaVeiculo.Controllers
{
    [Route("api/")]
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

        [HttpGet("colaboradores/{colaboradorId}/veiculos/{veiculoId}")]
        public async Task<IActionResult> GetByIdAsync(int colaboradorId, int veiculoId)
        {
            var colaboradorVeiculo = await _colaboradorVeiculoRepository
                .GetByIdAsync(colaboradorId, veiculoId);

            if(colaboradorVeiculo is null)
            {
                return NotFound("Vínculo entre Colaborador e Veículo não econtrado.");
            }

            return Ok(colaboradorVeiculo);
        }

        [HttpGet("colaboradores/veiculos")]
        public async Task<IActionResult> GetAllAsync()
        {
            var colaboradoresVeiculos = await _colaboradorVeiculoRepository.GetAllAsync();

            if(colaboradoresVeiculos.Count == 0)
            {
                return NotFound("Nenhum vínculo entre Colaboradores e Veículos foram econtrados.");
            }

            return Ok(colaboradoresVeiculos);
        }

        [HttpPost("colaboradores/{colaboradorId}/veiculos/{veiculoId}")]
        public async Task<IActionResult> CreateAsync([FromBody] ColaboradorVeiculo colaboradorVeiculo, int colaboradorId, int veiculoId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Estado do modelo inválido.");
            }

            if(colaboradorVeiculo.ColaboradorId != colaboradorId
                || colaboradorVeiculo.VeiculoId != veiculoId)
            {
                return BadRequest("Os id's da requisição e da url não condizem.");
            }

            var colaborador = await _colaboradorRepository.GetByIdAsync(colaboradorId);
            var veiculo = await _veiculoRepository.GetByIdAsync(veiculoId);

            if(colaborador is null || veiculo is null)
            {
                return NotFound("Vínculo entre Colaborador e Veículo não econtrado.");
            }

            colaboradorVeiculo.Colaborador = colaborador;
            colaboradorVeiculo.Veiculo = veiculo;
            colaboradorVeiculo.DataInicioVinculo = DateTimeOffset.Now;

            await _colaboradorVeiculoRepository.CreateAsync(colaboradorVeiculo);

            return Ok(colaboradorVeiculo);
        }

        [HttpPut("colaboradores/{colaboradorId}/veiculos/{veiculoId}")]
        public async Task<IActionResult> UpdateAsync([FromBody] ColaboradorVeiculo colaboradorVeiculoRequisicao, int colaboradorId, int veiculoId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Estado do modelo inválido.");
            }

            if (colaboradorVeiculoRequisicao.ColaboradorId != colaboradorId
                || colaboradorVeiculoRequisicao.VeiculoId != veiculoId)
            {
                return BadRequest("Os id's da requisição e da url não condizem.");
            }

            var colaboradorVeiculoDb = 
                await _colaboradorVeiculoRepository.GetByIdAsync(colaboradorId, veiculoId);

            if(colaboradorVeiculoDb is null)
            {
                return NotFound("Vínculo entre Colaborador e Veículo não econtrado.");
            }

            await _colaboradorVeiculoRepository
                .UpdateAsync(colaboradorVeiculoRequisicao, colaboradorVeiculoDb);

            return Ok(colaboradorVeiculoRequisicao);
        }

        [HttpDelete("colaboradores/{colaboradorId}/veiculos/{veiculoId}")]
        public async Task<IActionResult> DeleteAsync(int colaboradorId, int veiculoId)
        {
            var colaboradorVeiculo = await _colaboradorVeiculoRepository.GetByIdAsync(colaboradorId, veiculoId);

            if(colaboradorVeiculo is null)
            {
                return NotFound("Vinculo entre Colaborador e Veículo não econtrado.");
            }

            await _colaboradorVeiculoRepository.DeleteAsync(colaboradorVeiculo);

            return Ok(colaboradorVeiculo);
        }
    }
}
