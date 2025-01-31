using GerenciamentoFrotaVeiculo.Api.Hypermedia.Filters;
using GerenciamentoFrotaVeiculo.Api.Repository.IRepository;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoFrotaVeiculo.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculosController(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id < 1)
            {
                return BadRequest(new { message = "Id inválido.", erroCode = "BAD_REQUEST" });
            }

            var veiculo = await _veiculoRepository.GetByIdAsync(id);

            if (veiculo is null)
            {
                return NotFound(new { message = "Veículo não encontado.", erroCode = "VEICULO_NOT_FOUND" });
            }

            return Ok(veiculo);
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> GetAllAsync()
        {
            var veiculos = await _veiculoRepository.GetAllAsync();

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

            var vo = await _veiculoRepository.CreateAsync(veiculoVO);

            return Ok(vo);
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> UpdateAsync([FromBody] VeiculoVO veiculoRequisicaoVO, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Estado do modelo inválido.", erroCode = "BAD_REQUEST" });
            }

            if (veiculoRequisicaoVO.Id != id || id < 1)
            {
                return BadRequest(new { message = "Os id's da requisição e da url não condizem ou são inválidos.", erroCode = "BAD_REQUEST" });
            }

            var veiculoDbVO = await _veiculoRepository.GetByIdAsync(id);

            if (veiculoDbVO is null)
            {
                return NotFound(new { message = "Veículo não encontado.", erroCode = "VEICULO_NOT_FOUND" });
            }

            var vo = await _veiculoRepository.UpdateAsync(veiculoDbVO, veiculoRequisicaoVO);

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

            var veiculoVO = await _veiculoRepository.GetByIdAsync(id);

            if (veiculoVO is null)
            {
                return NotFound(new { message = "Veículo não encontado.", erroCode = "VEICULO_NOT_FOUND" });
            }

            var responsta = await _veiculoRepository.DeleteAsync(veiculoVO);

            return Ok(responsta);
        }
    }
}
