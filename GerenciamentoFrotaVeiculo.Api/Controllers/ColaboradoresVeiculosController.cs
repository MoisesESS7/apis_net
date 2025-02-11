using GerenciamentoFrotaVeiculo.Api.Business;
using GerenciamentoFrotaVeiculo.Api.Hypermedia.Filters;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoFrotaVeiculo.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ColaboradoresVeiculosController : ControllerBase
    {
        private readonly IColaboradorVeiculoBusiness _colaboradorVeiculoBusiness;

        public ColaboradoresVeiculosController(IColaboradorVeiculoBusiness colaboradorVeiculoBusiness)
        {
            _colaboradorVeiculoBusiness = colaboradorVeiculoBusiness ?? throw new ArgumentNullException(nameof(colaboradorVeiculoBusiness));
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> FindByIdAsync(int id)
        {
            if (id < 1)
            {
                return BadRequest(new { message = "Id inválido.", erroCode = "BAD_REQUEST" });
            }

            var vo = await _colaboradorVeiculoBusiness.FindByIdAsync(id);

            if (vo is null)
            {
                return NotFound(new { message = "Vínculo entre Colaborador e Veículo não econtrado.", erroCode = "VINCULO_NOT_FOUND" });
            }

            return Ok(vo);
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> FindAllAsync()
        {
            var vo = await _colaboradorVeiculoBusiness.FindAllAsync();

            if (vo is null)
            {
                return NotFound(new { message = "Nenhum vínculo entre Colaboradores e Veículos foram econtrados.", erroCode = "VINCULOS_NOT_FOUND" });
            }

            return Ok(vo);
        }
        
        [HttpGet("buscar-por-data{data}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> FindBiDataAsync(DateTime data)
        {
            var vo = await _colaboradorVeiculoBusiness.FindByDataAsync(data);

            if (vo is null)
            {
                return NotFound(new { message = "Nenhum vínculo entre Colaboradores e Veículos foram econtrados neste período.", erroCode = "VINCULOS_NOT_FOUND" });
            }

            return Ok(vo);
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> CreateAsync([FromBody] ColaboradorVeiculoVO colaboradorVeiculoVO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Estado do modelo inválido.", erroCode = "BAD_REQUEST" });
            }

            if (colaboradorVeiculoVO.ColaboradorId < 1 || colaboradorVeiculoVO.VeiculoId < 1)
            {
                return BadRequest(new { message = "Os id's são necessários para esta operação.", erroCode = "BAD_REQUEST" });
            }

            var vo = await _colaboradorVeiculoBusiness.CreateAsync(colaboradorVeiculoVO);

            if (vo is null)
            {
                return BadRequest(new { message = "Erro ao cria vínculo.", erroCode = "BAD_REQUEST" });
            }

            return Ok(vo);
        }

        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> UpdateAsync([FromBody] ColaboradorVeiculoVO colaboradorVeiculoVO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Estado do modelo inválido.", erroCode = "BAD_REQUEST" });
            }

            if (colaboradorVeiculoVO.ColaboradorId < 1 || colaboradorVeiculoVO.VeiculoId < 1)
            {
                return BadRequest(new { message = "Os id's são necessários para esta operação.", erroCode = "BAD_REQUEST" });
            }

            var vo = await _colaboradorVeiculoBusiness.UpdateAsync(colaboradorVeiculoVO);

            if (vo is null)
            {
                return BadRequest(new { message = "Erro ao atualizar vínculo.", erroCode = "BAD_REQUEST" });
            }

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

            var resposta = await _colaboradorVeiculoBusiness.DeleteByIdAsync(id);

            if (resposta)
            {
                return BadRequest(new { message = "Erro ao deletar vínculo.", erroCode = "BAD_REQUEST" });
            }

            return Ok(resposta);
        }
    }
}
