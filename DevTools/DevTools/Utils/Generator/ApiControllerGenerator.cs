using DevTools.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Utils.Generator;

public class ApiControllerGenerator : IFileGenerator
{
    public ApiControllerGenerator()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Iniciando geração de Controller API...");
        Console.ResetColor();
    }

    public void Generate(string entityName)
    {
        string controllerName = $"{entityName}Controller.cs";

        StringBuilder apiControllerContent = new StringBuilder();

        apiControllerContent.AppendLine("using CRM.API.Integracao.Services;");
        apiControllerContent.AppendLine("using Microsoft.AspNetCore.Mvc;");
        apiControllerContent.AppendLine("using System.Net.Mime;");
        apiControllerContent.AppendLine("using Swashbuckle.AspNetCore.Annotations;");
        apiControllerContent.AppendLine("using Microsoft.AspNetCore.Authorization;");
        apiControllerContent.AppendLine();
        apiControllerContent.AppendLine("namespace CRM.API.Controllers");
        apiControllerContent.AppendLine("{");
        apiControllerContent.AppendLine("    [ApiController]");
        apiControllerContent.AppendLine("    [Route(\"api/[controller]\")]");
        apiControllerContent.AppendLine("    [ApiExplorerSettings(GroupName = \"Cadastros Básicos\")]");
        apiControllerContent.AppendLine("    [Authorize(\"Bearer\")]");
        apiControllerContent.AppendLine("    [SwaggerControllerOrder(100)]");
        apiControllerContent.AppendLine("#if DEBUG");
        apiControllerContent.AppendLine("    [AllowAnonymous]");
        apiControllerContent.AppendLine("#endif");
        apiControllerContent.AppendLine($"    public class {entityName}Controller : ControllerBase");
        apiControllerContent.AppendLine("    {");
        apiControllerContent.AppendLine($"        private readonly IAbstractServiceChavesERP<{entityName}, {entityName}DTO> _Service;");
        apiControllerContent.AppendLine();
        apiControllerContent.AppendLine($"        public {entityName}Controller(IAbstractServiceChavesERP<{entityName}, {entityName}DTO> service)");
        apiControllerContent.AppendLine("        {");
        apiControllerContent.AppendLine("            _Service = service;");
        apiControllerContent.AppendLine("        }");
        apiControllerContent.AppendLine();
        apiControllerContent.AppendLine("        /// <summary>");
        apiControllerContent.AppendLine("        /// Recuperar lista de registros dos dados previamente integrados");
        apiControllerContent.AppendLine("        /// </summary>");
        apiControllerContent.AppendLine("        [HttpGet]");
        apiControllerContent.AppendLine($"        public async Task<ActionResult<IEnumerable<{entityName}DTO>>> Get(string? chaves{entityName}, int? offset, int? limit)");
        apiControllerContent.AppendLine("        {");
        apiControllerContent.AppendLine("            try");
        apiControllerContent.AppendLine("            {");
        apiControllerContent.AppendLine($"                if (!string.IsNullOrWhiteSpace(chaves{entityName}))");
        apiControllerContent.AppendLine("                {");
        apiControllerContent.AppendLine($"                    var filtradas = await _Service.GetPorChaveERPListAsync(offset, limit, ServiceUtils.StringToList(chaves{entityName}));");
        apiControllerContent.AppendLine("                    return Ok(filtradas);");
        apiControllerContent.AppendLine("                }");
        apiControllerContent.AppendLine();
        apiControllerContent.AppendLine("                var lista = await _Service.GetAllAsync(offset, limit);");
        apiControllerContent.AppendLine("                return Ok(lista);");
        apiControllerContent.AppendLine("            }");
        apiControllerContent.AppendLine("            catch (Exception ex)");
        apiControllerContent.AppendLine("            {");
        apiControllerContent.AppendLine("                return BadRequest(new");
        apiControllerContent.AppendLine("                {");
        apiControllerContent.AppendLine("                    Errors = new List<object> { new { Error = ex } }");
        apiControllerContent.AppendLine("                });");
        apiControllerContent.AppendLine("            }");
        apiControllerContent.AppendLine("        }");
        apiControllerContent.AppendLine();
        apiControllerContent.AppendLine("        /// <summary>");
        apiControllerContent.AppendLine("        /// Permite integrar uma lista de registros");
        apiControllerContent.AppendLine("        /// </summary>");
        apiControllerContent.AppendLine("        [HttpPost]");
        apiControllerContent.AppendLine("        [Consumes(MediaTypeNames.Application.Json)]");
        apiControllerContent.AppendLine("        [ProducesResponseType(StatusCodes.Status200OK)]");
        apiControllerContent.AppendLine("        [ProducesResponseType(StatusCodes.Status204NoContent)]");
        apiControllerContent.AppendLine("        [ProducesResponseType(StatusCodes.Status400BadRequest)]");
        apiControllerContent.AppendLine($"        public async Task<IActionResult> Post([FromBody] IEnumerable<{entityName}DTO> items)");
        apiControllerContent.AppendLine("        {");
        apiControllerContent.AppendLine("            if (!items.Any())");
        apiControllerContent.AppendLine("                return NoContent();");
        apiControllerContent.AppendLine();
        apiControllerContent.AppendLine("            var ok = new List<object>();");
        apiControllerContent.AppendLine("            var errors = new List<object>();");
        apiControllerContent.AppendLine();
        apiControllerContent.AppendLine("            foreach (var item in items)");
        apiControllerContent.AppendLine("            {");
        apiControllerContent.AppendLine("                try");
        apiControllerContent.AppendLine("                {");
        apiControllerContent.AppendLine("                    ok.Add(await _Service.SaveAsync(item));");
        apiControllerContent.AppendLine("                }");
        apiControllerContent.AppendLine("                catch (Exception ex)");
        apiControllerContent.AppendLine("                {");
        apiControllerContent.AppendLine("                    errors.Add(new { Error = ex, Item = item });");
        apiControllerContent.AppendLine("                }");
        apiControllerContent.AppendLine("            }");
        apiControllerContent.AppendLine();
        apiControllerContent.AppendLine("            if (errors.Any())");
        apiControllerContent.AppendLine("            {");
        apiControllerContent.AppendLine("                return BadRequest(new");
        apiControllerContent.AppendLine("                {");
        apiControllerContent.AppendLine("                    OK = ok,");
        apiControllerContent.AppendLine("                    OKCount = ok.Count,");
        apiControllerContent.AppendLine("                    Errors = errors,");
        apiControllerContent.AppendLine("                    ErrorsCount = errors.Count");
        apiControllerContent.AppendLine("                });");
        apiControllerContent.AppendLine("            }");
        apiControllerContent.AppendLine();
        apiControllerContent.AppendLine("            return Ok(new");
        apiControllerContent.AppendLine("            {");
        apiControllerContent.AppendLine("                OK = ok,");
        apiControllerContent.AppendLine("                OKCount = ok.Count");
        apiControllerContent.AppendLine("            });");
        apiControllerContent.AppendLine("        }");
        apiControllerContent.AppendLine("    }");
        apiControllerContent.AppendLine("}");

        string tempPath = Path.Combine(Path.GetTempPath(), $"{entityName}DTO.cs");
        File.WriteAllText(tempPath, apiControllerContent.ToString(), Encoding.UTF8);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"API Controller gerado com sucesso: {tempPath}");
        Console.ResetColor();

        Process.Start("notepad.exe", tempPath);
    }
}
