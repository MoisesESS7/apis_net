using GerenciamentoFrotaVeiculo.Api.Data.Contract;
using GerenciamentoFrotaVeiculo.Api.Hypermedia.Filters;
using GerenciamentoFrotaVeiculo.Api.Repository.IRepository;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using GerenciamentoFrotaVeiculo.Models;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoFrotaVeiculo.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ColaboradoresVeiculosController : ControllerBase
    {
        private readonly IColaboradorVeiculoRepository _colaboradorVeiculoRepository;
        private readonly IColaboradorRepository _colaboradorRepository;
        private readonly IParser<ColaboradorVO, Colaborador> _voToColaboradorParse;
        private readonly IVeiculoRepository _veiculoRepository;

        public ColaboradoresVeiculosController(IColaboradorVeiculoRepository colaboradorVeiculoRepository,
            IColaboradorRepository colaboradorRepository, IParser<ColaboradorVO, Colaborador> voToColaboradorParse,
            IVeiculoRepository veiculoRepository)
        {
            _colaboradorVeiculoRepository = colaboradorVeiculoRepository ?? throw new ArgumentNullException(nameof(colaboradorVeiculoRepository));
            _colaboradorRepository = colaboradorRepository ?? throw new ArgumentNullException(nameof(colaboradorRepository));
            _voToColaboradorParse = voToColaboradorParse ?? throw new ArgumentNullException(nameof(voToColaboradorParse));
            _veiculoRepository = veiculoRepository ?? throw new ArgumentNullException(nameof(veiculoRepository));
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id < 1)
            {
                return BadRequest(new { message = "Id inválido.", erroCode = "BAD_REQUEST" });
            }

            var colaboradorVeiculo = await _colaboradorVeiculoRepository
                .GetByIdAsync(id);

            if (colaboradorVeiculo is null)
            {
                return NotFound(new { message = "Vínculo entre Colaborador e Veículo não econtrado.", erroCode = "VINCULO_NOT_FOUND" });
            }

            return Ok(colaboradorVeiculo);
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> GetAllAsync()
        {
            var colaboradoresVeiculosVO = await _colaboradorVeiculoRepository.GetAllAsync();

            if (colaboradoresVeiculosVO.Count == 0)
            {
                return NotFound(new { message = "Nenhum vínculo entre Colaboradores e Veículos foram econtrados.", erroCode = "VINCULOS_NOT_FOUND" });
            }

            return Ok(colaboradoresVeiculosVO);
        }

        [HttpPost("{colaboradorId}/{veiculoId}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> CreateAsync([FromBody] ColaboradorVeiculoVO colaboradorVeiculoVO, int colaboradorId, int veiculoId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Estado do modelo inválido.", erroCode = "BAD_REQUEST" });
            }

            if (colaboradorVeiculoVO.ColaboradorId != colaboradorId
                || colaboradorVeiculoVO.VeiculoId != veiculoId
                || colaboradorVeiculoVO.VeiculoId < 1)
            {
                return BadRequest(new { message = "Os id's da requisição e da url não condizem ou são inválidos.", erroCode = "BAD_REQUEST" });
            }

            var vo = await _colaboradorVeiculoRepository.CreateAsync(colaboradorVeiculoVO);

            if (vo is null)
            {
                return BadRequest(new { message = "Erro ao cria vinculo.", erroCode = "BAD_REQUEST" });
            }

            return Ok(vo);
        }

        [HttpPut("{colaboradorId}/{veiculoId}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> UpdateAsync([FromBody] ColaboradorVeiculoVO colaboradorVeiculoRequisicaoVO, int colaboradorId, int veiculoId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Estado do modelo inválido.", erroCode = "BAD_REQUEST" });
            }

            if (colaboradorVeiculoRequisicaoVO.ColaboradorId != colaboradorId
                || colaboradorVeiculoRequisicaoVO.VeiculoId != veiculoId
                || colaboradorVeiculoRequisicaoVO.VeiculoId < 1)
            {
                return BadRequest(new { message = "Os id's da requisição e da url não condizem ou são inválidos.", erroCode = "BAD_REQUEST" });
            }

            var colaboradorVeiculoDbVO =
                await _colaboradorVeiculoRepository.GetByIdAsync(colaboradorVeiculoRequisicaoVO.Id);

            if (colaboradorVeiculoDbVO is null)
            {
                return NotFound(new { message = "Vínculo entre Colaborador e Veículo não econtrado.", erroCode = "VINCULO_NOT_FOUND" });
            }

            var vo = await _colaboradorVeiculoRepository
                .UpdateAsync(colaboradorVeiculoRequisicaoVO, colaboradorVeiculoDbVO);

            return Ok(vo);
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id < 1)
            {
                return BadRequest(new { message = "Id inválido.", erroCode = "BAD_REQUEST" });
            }

            var colaboradorVeiculoVO = await _colaboradorVeiculoRepository.GetByIdAsync(id);

            if (colaboradorVeiculoVO is null)
            {
                return NotFound(new { message = "Vinculo entre Colaborador e Veículo não econtrado.", errorCode = "COLABORADOR_NOT_FOUND" });
            }

            var resposta = await _colaboradorVeiculoRepository.DeleteAsync(colaboradorVeiculoVO);

            return Ok(resposta);
        }
    }
}
