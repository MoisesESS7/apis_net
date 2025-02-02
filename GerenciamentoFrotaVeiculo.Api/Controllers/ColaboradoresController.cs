using GerenciamentoFrotaVeiculo.Api.Business;
using GerenciamentoFrotaVeiculo.Api.Hypermedia.Filters;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoFrotaVeiculo.Controllers
{
    [ApiVersion("1")]
    //[ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ColaboradoresController : ControllerBase
    {
        private readonly IColaboradorBusiness _colaboradorBusiness;

        public ColaboradoresController(IColaboradorBusiness colaboradorRepository)
        {
            _colaboradorBusiness = colaboradorRepository;
        }

        //[ApiVersion("2")]
        [HttpGet("buscar-por-nome")]
        [ProducesResponseType(200, Type = typeof(List<ColaboradorVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> FindByNameAsync([FromQuery] string? nome)
        {
            var vo = await _colaboradorBusiness.FindByNameAsync(nome!);

            if (vo is null || !vo.Any())
            {
                return NotFound(new { message = "Colaborador não encontrado.", errorCode = "COLABORADOR_NOT_FOUND" });
            }

            return Ok(vo);
        }
        
        //[ApiVersion("2")]
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ColaboradorVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> FindByIdAsync(int id)
        {
            if (id < 1)
            {
                return BadRequest(new { message = "Id inválido.", erroCode = "BAD_REQUEST" });
            }

            var vo = await _colaboradorBusiness.FindByIdAsync(id);

            if (vo is null)
            {
                return NotFound(new { message = "Colaborador não encontrado.", errorCode = "COLABORADOR_NOT_FOUND" });
            }

            return Ok(vo);
        }

        [HttpGet("incluir-veiculos/{id}")]
        [ProducesResponseType(200, Type = typeof(ColaboradorVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> FindByIdIncludeVeiculosAsync(int id)
        {            
            if (id < 1)
            {
                return BadRequest(new { message = "Id inválido.", erroCode = "BAD_REQUEST" });
            }

            var vo = await _colaboradorBusiness.FindByIdIncludeVeiculosAsync(id);

            if (vo is null)
            {
                return NotFound(new { message = "Colaborador não encontrado.", errorCode = "COLABORADOR_NOT_FOUND" });
            }

            return Ok(vo);
        }

        [HttpGet("incluir-veiculos")]
        [ProducesResponseType(200, Type = typeof(List<ColaboradorVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> FindAllIncludeVeiculosAsync()
        {

            var vo = await _colaboradorBusiness.FindAllIncludeVeiculosAsync();

            if (vo.Count == 0)
            {
                return NotFound(new { message = "Nenhum colaborador foi encontrado.", errorCode = "COLABORADOR_NOT_FOUND" });
            }

            return Ok(vo);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ColaboradorVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> FindAllAsync()
        {
            var vo = await _colaboradorBusiness.FindAllAsync();

            if (vo is null)
            {
                return NotFound(new { message = "Nenhum colaborador foi encontrado.", errorCode = "COLABORADOR_NOT_FOUND" });
            }

            return Ok(vo);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ColaboradorVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> CreateAsync([FromBody] ColaboradorVO colaboradorVO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Estado do modelo inválido.", erroCode = "BAD_REQUEST" });
            }

            var vo = await _colaboradorBusiness.CreateAsync(colaboradorVO);

            return Ok(vo);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ColaboradorVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> UpdateAsync([FromBody] ColaboradorVO colaboradorVO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Estado do modelo inválido.", erroCode = "BAD_REQUEST" });
            }

            var vo = await _colaboradorBusiness.UpdateAsync(colaboradorVO);

            if (vo is null)
            {
                return BadRequest(new { message = "Operação atualizar falhou.", errorCode = "BAD_REQUEST" });
            }

            return Ok(vo);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204, Type = typeof(ColaboradorVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id < 1)
            {
                return BadRequest(new { message = "Id inválido.", erroCode = "BAD_REQUEST" });
            }

            var responsta = await _colaboradorBusiness.DeleteByIdAsync(id);
                        
            if (!responsta)
            {
                return NotFound(new { message = "Colaborador não encontrado.", errorCode = "COLABORADOR_NOT_FOUND" });
            }

            return NoContent();
        }
    }
}
