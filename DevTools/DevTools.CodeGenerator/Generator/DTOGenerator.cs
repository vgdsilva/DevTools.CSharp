using DevTools.CodeGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevTools.CodeGenerator.Generator;

public class DTOGenerator : IFileGenerator
{
    public DTOGenerator()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Iniciando geração de DTO...");
        Console.ResetColor();
    }

    public void Generate(string path)
    {
        try
        {
            string filePath = path;
            if ( !File.Exists(filePath) )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Arquivo não encontrado.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"Lendo arquivo: {filePath}");

            var fileContent = File.ReadAllText(filePath);
            var lines = File.ReadAllLines(filePath).ToList();
            var propertiesOutput = new StringBuilder();

            string? className = GetClassName(fileContent);
            if ( string.IsNullOrEmpty(className) )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Classe não encontrada no arquivo.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"Classe detectada: {className}");

            var regex = new Regex(@"((\s*\[[^\]]+\])*\s*public\s+[^\s]+\s+\w+\s*\{[^}]*\})", RegexOptions.Multiline);
            bool hasKeyProperty = false;
            int propriedadesIncluidas = 0;

            foreach ( Match match in regex.Matches(fileContent) )
            {
                string propBlock = match.Groups[1].Value;

                if ( propBlock.Contains("override") ||
                    propBlock.Contains("[NotMapped]") ||
                    propBlock.Contains("[ForeignKey]") ||
                    propBlock.Contains("[DatabaseGenerated") )
                    continue;

                var propNameMatch = Regex.Match(propBlock, @"public\s+[^\s]+\s+(\w+)");
                var propTypeMatch = Regex.Match(propBlock, @"public\s+([^\s]+)\s+\w+");

                if ( !propNameMatch.Success || !propTypeMatch.Success )
                    continue;

                string originalPropName = propNameMatch.Groups[1].Value;
                string propType = propTypeMatch.Groups[1].Value;
                string propName = originalPropName;
                var attributes = new List<string>();

                var attributeMatches = Regex.Matches(propBlock, @"\[[^\]]+\]");
                foreach ( Match attr in attributeMatches )
                {
                    string attrValue = attr.Value;

                    if ( attrValue.StartsWith("[Column") ) continue;
                    if ( attrValue.StartsWith("[StringLength") )
                        attributes.Add(attrValue);
                }

                string SwaggerSchemaDescription = originalPropName.Contains("ID") ? "Chave primária do registro no ERP, esta chave deve identificar de forma única o registro." : "";

                attributes.Add($@"        [SwaggerSchema(Description = ""{SwaggerSchemaDescription}"")]");

                if ( originalPropName.Contains("ChaveERP") )
                    continue;

                bool isNullable = propType.EndsWith("?");
                bool isCustomEntity = Char.IsUpper(propType[0]) && !propType.StartsWith("string");

                if ( originalPropName.Contains("ID") )
                {
                    propName = "Chave" + originalPropName.Substring(2);
                    propType = "string";
                    attributes.Add("[MaxLength(60)]");
                    Console.WriteLine($"→ Transformando '{originalPropName}' em chave: '{propName}'");
                }

                if ( isCustomEntity && !isNullable )
                {
                    attributes.Add($@"[Required(ErrorMessage = ""{propName} é obrigatório"")]");
                }

                propertiesOutput.AppendLine(string.Join("\n         ", attributes) + $"\n           public {propType} {propName} {{ get; set; }}\n");
                propriedadesIncluidas++;
            }

            var dtoContent = new StringBuilder();
            dtoContent.AppendLine("using System.ComponentModel.DataAnnotations;");
            dtoContent.AppendLine("using Swashbuckle.AspNetCore.Annotations;");
            dtoContent.AppendLine();
            dtoContent.AppendLine("namespace CRM.API.Integracao.DTOs");
            dtoContent.AppendLine("{");
            dtoContent.AppendLine($"    public class {className}DTO");
            dtoContent.AppendLine("    {");
            dtoContent.AppendLine($@"           [Key]");
            dtoContent.AppendLine($@"           [SwaggerSchema(Description = ""Chave primária do registro no ERP, esta chave deve identificar de forma única o registro."")]");
            dtoContent.AppendLine($@"           [Required(ErrorMessage = ""Chave{className} é obrigatório"")]");
            dtoContent.AppendLine($@"           [MaxLength(60)]");
            dtoContent.AppendLine($"           public string Chave{className} {{ get; set; }}\n");
            dtoContent.Append(propertiesOutput);
            dtoContent.AppendLine("     }");
            dtoContent.AppendLine("}");

            string tempPath = Path.Combine(Path.GetTempPath(), $"{className}DTO.cs");
            File.WriteAllText(tempPath, dtoContent.ToString(), Encoding.UTF8);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"DTO gerado com sucesso: {tempPath}");
            Console.WriteLine($"Propriedades incluídas: {propriedadesIncluidas}");
            Console.ResetColor();

            Process.Start("notepad.exe", tempPath);
        }
        catch ( Exception ex )
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: {ex.Message}");
            Console.ResetColor();
        }
    }

    static string? GetClassName(string content)
    {
        var match = Regex.Match(content, @"public\s+(partial\s+)?class\s+(\w+)");
        return match.Success ? match.Groups[2].Value : null;
    }
}
