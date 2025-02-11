using GerenciamentoFrotaVeiculo.Api.Business;
using GerenciamentoFrotaVeiculo.Api.Hypermedia.Filters;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

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

        [HttpGet("buscar-por-ano/{ano}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> BuscarPorAnoAsync(int ano)
        {
            var vo = await _veiculoBusiness.BuscarVeiculosPorAnoAsync(ano);

            if (vo is null || !vo.Any())
            {
                return NotFound(new { message = "Nenhum veículo foi encontrado.", erroCode = "VEICULOS_NOT_FOUND" });
            }

            return Ok(vo);
        }

        [HttpGet("buscar-por-modelo/{modelo}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> BuscarPorModeloAsync(string modelo)
        {
            var vo = await _veiculoBusiness.BuscarVeiculosPorModeloAsync(modelo);

            if (vo is null || !vo.Any())
            {
                return NotFound(new { message = "Nenhum veículo foi encontrado.", erroCode = "VEICULOS_NOT_FOUND" });
            }

            return Ok(vo);
        }

        [HttpGet("buscar-por-categoria/{categoria}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> BuscarPorCategoriaAsync(string categoria)
        {
            var vo = await _veiculoBusiness.BuscarVeiculosPorCategoriaAsync(categoria);

            if (vo is null || !vo.Any())
            {
                return NotFound(new { message = "Nenhum veículo foi encontrado.", erroCode = "VEICULOS_NOT_FOUND" });
            }

            return Ok(vo);
        }

        [HttpGet("buscar-por-placa/{placa}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> BuscarPorPlacaAsync(string placa)
        {
            var vo = await _veiculoBusiness.BuscarVeiculosPorPlacaAsync(placa);

            if (vo is null || !vo.Any())
            {
                return NotFound(new { message = "Nenhum veículo foi encontrado.", erroCode = "VEICULOS_NOT_FOUND" });
            }

            return Ok(vo);
        }

        [HttpGet("buscar-veiculos-licenciados")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> BuscarVeiculosLicenciadosAsync()
        {
            var vo = await _veiculoBusiness.BuscarVeiculosPorLicenciamentoAsync(true);

            if (vo is null || !vo.Any())
            {
                return NotFound(new { message = "Nenhum veículo foi encontrado.", erroCode = "VEICULOS_NOT_FOUND" });
            }

            return Ok(vo);
        }

        [HttpGet("buscar-veiculos-nao-licenciados")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> BuscarVeiculosLicenciamentoAtrasadoAsync()
        {
            var vo = await _veiculoBusiness.BuscarVeiculosPorLicenciamentoAsync(false);

            if (vo is null || !vo.Any())
            {
                return NotFound(new { message = "Nenhum veículo foi encontrado.", erroCode = "VEICULOS_NOT_FOUND" });
            }

            return Ok(vo);
        }

        [HttpGet("buscar-por-marca/{marca}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> BuscarVeiculosPorMarcaAsync(string marca)
        {
            var vo = await _veiculoBusiness.BuscarVeiculosPorMarcaAsync(marca);

            if (vo is null || !vo.Any())
            {
                return NotFound(new { message = "Nenhum veículo foi encontrado.", erroCode = "VEICULOS_NOT_FOUND" });
            }

            return Ok(vo);
        }

        [HttpGet("buscar-por-cor/{cor}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> BuscarVeiculosPorCorAsync(string cor)
        {
            var vo = await _veiculoBusiness.BuscarVeiculosPorCorAsync(cor);

            if (vo is null || !vo.Any())
            {
                return NotFound(new { message = "Nenhum veículo foi encontrado.", erroCode = "VEICULOS_NOT_FOUND" });
            }

            return Ok(vo);
        }

        [HttpGet("buscar-por-quilometragem")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> BuscarVeiculosPorQuilometragemAsync([FromQuery] int quilometragemMinima, int quilometragemMaxima)
        {
            var vo = await _veiculoBusiness.BuscarVeiculosPorQuilometragemAsync(quilometragemMinima, quilometragemMaxima);

            if (vo is null || !vo.Any())
            {
                return NotFound(new { message = "Nenhum veículo foi encontrado.", erroCode = "VEICULOS_NOT_FOUND" });
            }

            return Ok(vo);
        }

        [HttpGet("buscar-por-query")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> BuscarVeiculosPorQueryAsync([FromQuery] string? placa, [FromQuery] string? marca,
        [FromQuery] int quilometragemMinima, [FromQuery] int quilometragemMaxima, [FromQuery] string? cor, [FromQuery] int ano,
        [FromQuery] string? modelo, [FromQuery] string? categoria, [FromQuery] bool licenciamento)
        {
            var vo = await _veiculoBusiness.BuscarVeiculosAsync(placa, marca, quilometragemMinima,
                    quilometragemMaxima, cor, ano, modelo, categoria, licenciamento);

            if (vo is null || !vo.Any())
            {
                return NotFound(new { message = "Nenhum veículo foi encontrado.", erroCode = "VEICULOS_NOT_FOUND" });
            }

            return Ok(vo);
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
