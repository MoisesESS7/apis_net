using GerenciamentoFrotaVeiculo.Api.Business;
using GerenciamentoFrotaVeiculo.Api.Hypermedia.Filters;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoFrotaVeiculo.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly IVeiculoBusiness _veiculoBusiness;

        public VeiculosController(IVeiculoBusiness veiculoBusiness)
        {
            _veiculoBusiness = veiculoBusiness;
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> FindByIdAsync(int id)
        {
            if (id < 1)
            {
                return BadRequest(new { message = "Id inválido.", erroCode = "BAD_REQUEST" });
            }

            var veiculo = await _veiculoBusiness.FindByIdAsync(id);

            if (veiculo is null)
            {
                return NotFound(new { message = "Veículo não encontado.", erroCode = "VEICULO_NOT_FOUND" });
            }

            return Ok(veiculo);
        }
        
        [HttpGet("busca-completa/{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> FindByIdIncludeColaboradoresAsync(int id)
        {
            if (id < 1)
            {
                return BadRequest(new { message = "Id inválido.", erroCode = "BAD_REQUEST" });
            }

            var veiculo = await _veiculoBusiness.FindByIdIncludeColaboradoresAsync(id);

            if (veiculo is null)
            {
                return NotFound(new { message = "Veículo não encontado.", erroCode = "VEICULO_NOT_FOUND" });
            }

            return Ok(veiculo);
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> FindAllAsync()
        {
            var veiculos = await _veiculoBusiness.FindAllAsync();

            if (veiculos.Count == 0)
            {
                return NotFound(new { message = "Nenhum veículo foi encontrado.", erroCode = "VEICULOS_NOT_FOUND" });
            }

            return Ok(veiculos);
        }
        
        [HttpGet("busca-completa")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> FindAllIncludeColaboradoresAsync()
        {
            var veiculos = await _veiculoBusiness.FindAllIncludeColaboradoresAsync();

            if (veiculos.Count == 0)
            {
                return NotFound(new { message = "Nenhum veículo foi encontrado.", erroCode = "VEICULOS_NOT_FOUND" });
            }

            return Ok(veiculos);
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> CreateAsync([FromBody] VeiculoVO veiculoVO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Estado do modelo inválido.", erroCode = "BAD_REQUEST" });
            }

            var vo = await _veiculoBusiness.CreateAsync(veiculoVO);

            return Ok(vo);
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> UpdateAsync([FromBody] VeiculoVO veiculoVO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Estado do modelo inválido.", erroCode = "BAD_REQUEST" });
            }

            var vo = await _veiculoBusiness.UpdateAsync(veiculoVO);

            if (vo is null)
            {
                return BadRequest(new { message = "Operação atualizar falhou.", errorCode = "BAD_REQUEST" });
            }

            return Ok(vo);
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id < 1)
            {
                return BadRequest(new { MESSAGE = "Id inválido.", erroCode = "BAD_REQUEST" });
            }

            var resposta = await _veiculoBusiness.DeleteAsync(id);

            if (resposta)
            {
                return NotFound(new { message = "Veículo não encontado.", erroCode = "VEICULO_NOT_FOUND" });
            }

            return NoContent();
        }
    }
}
