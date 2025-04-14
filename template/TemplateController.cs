using CRM.API.Integracao.Services;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "Cadastros Básicos")]
    [Authorize("Bearer")]
    [SwaggerControllerOrder(100)]
#if DEBUG
    [AllowAnonymous]
#endif
    public class {{_Entity}}Controller : ControllerBase
    {
        private readonly IAbstractServiceChavesERP<{{_Entity}}, {{_Entity}}DTO> _Service;

        public {{_Entity}}Controller(IAbstractServiceChavesERP<{{_Entity}}, {{_Entity}}DTO> service)
        {
            _Service = service;
        }

        /// <summary>
        /// Recuperar lista de registros dos dados previamente integrados
        /// </summary>
        /// <returns>Lista de registros que possui ChaveERP</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<{{_Entity}}DTO>>> Get(string? chaves{{_Entity}}, int? offset, int? limit)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(chaves{{_Entity}}))
                {
                    var {{_Entity}}Filtradas = await _Service.GetPorChaveERPListAsync(offset, limit, ServiceUtils.StringToList(chaves{{_Entity}}));
                    return Ok({{_Entity}}Filtradas);
                }

                var {{_Entity}}List = await _Service.GetAllAsync(offset, limit);
                return Ok({{_Entity}}List);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Errors = new List<object> { new { Error = ex } }
                });
            }
        }


        /// <summary>
        /// Permite integrar uma lista de registros
        /// </summary>
        /// <param name="items"></param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST /api/{{_Entity}}
        ///     [
        ///       {
        ///         "chave{{_Entity}}": "1"
        ///       }
        ///     ]
        /// </remarks>
        /// <response code="200">Retorna sucesso caso todos os items da lista foram integrados.</response>
        /// <response code="204">Retorna erro caso a lista é nula ou vazia.</response>
        /// <response code="400">Retorna erro em caso de exceção na execução da integração.</response>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] IEnumerable<{{_Entity}}DTO> items)
        {
            if (!items.Any())
            {
                return NoContent();
            }

            var ok = new List<object>();
            var errors = new List<object>();

            foreach (var item in items)
            {
                try
                {
                    ok.Add(await _Service.SaveAsync(item));
                }
                catch (Exception ex)
                {
                    errors.Add(new
                    {
                        Error = ex,
                        Item = item
                    });
                }
            }

            if (errors.Any())
            {
                return BadRequest(new
                {
                    OK = ok,
                    OKCount = ok.Count,
                    Errors = errors,
                    ErrorsCount = errors.Count
                });
            }

            return Ok(new
            {
                OK = ok,
                OKCount = ok.Count,
            });
        }
    }
}
