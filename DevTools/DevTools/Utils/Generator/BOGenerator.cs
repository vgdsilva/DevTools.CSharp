using DevTools.Utils.Interfaces;
using System.Diagnostics;
using System.Text;

namespace DevTools.Utils.Generator;

public class BOGenerator : IFileGenerator
{
    public BOGenerator()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Iniciando geração do BO...");
        Console.ResetColor();
    }

    public void Generate(string entityName)
    {
        string boClassName = $"{entityName}BO";
        string daoClassName = $"{entityName}DAO";
        string daoVariableName = $"_{entityName.ToCamelCase()}DAO";

        var boContent = new StringBuilder();

        boContent.AppendLine("using CRM.DAO;");
        boContent.AppendLine("using CRM.DAO.dao;");
        boContent.AppendLine("using CRM.DAO.interfaces;");
        boContent.AppendLine("using CRM.Entidades.entidades;");
        boContent.AppendLine("using System.Threading.Tasks;");
        boContent.AppendLine();
        boContent.AppendLine("namespace CRM.Regras.regras");
        boContent.AppendLine("{");
        boContent.AppendLine($"    public class {boClassName} : AbstractBO<{entityName}>");
        boContent.AppendLine("    {");
        boContent.AppendLine($"        private readonly {daoClassName} {daoVariableName};");
        boContent.AppendLine();
        boContent.AppendLine($"        public {boClassName}(ITransactionDB TransactionDB = null, DCSequenceController Controller = null) : base(TransactionDB, Controller)");
        boContent.AppendLine("        {");
        boContent.AppendLine($"            {daoVariableName} = new {daoClassName}(TransactionDB, Controller);");
        boContent.AppendLine("        }");
        boContent.AppendLine();
        boContent.AppendLine("        public override bool Remover(long id)");
        boContent.AppendLine("        {");
        boContent.AppendLine($"            {daoVariableName}.Remove(id);");
        boContent.AppendLine("            return true;");
        boContent.AppendLine("        }");
        boContent.AppendLine();
        boContent.AppendLine($"        public override bool Salvar({entityName} entity)");
        boContent.AppendLine("        {");
        boContent.AppendLine($"            if (entity.ID{entityName} == -1)");
        boContent.AppendLine("            {");
        boContent.AppendLine($"                entity.ID{entityName} = Controller.GetNextID(nameof({entityName}), nameof({entityName}.ID{entityName}), TransactionDB: TransactionDB);");
        boContent.AppendLine("            }");
        boContent.AppendLine();
        boContent.AppendLine($"            {daoVariableName}.SaveChanges(entity);");
        boContent.AppendLine("            return true;");
        boContent.AppendLine("        }");
        boContent.AppendLine();
        boContent.AppendLine($"        public override async Task<bool> SalvarAsync({entityName} entity)");
        boContent.AppendLine("        {");
        boContent.AppendLine($"            if (entity.ID{entityName} == -1)");
        boContent.AppendLine("            {");
        boContent.AppendLine($"                entity.ID{entityName} = Controller.GetNextID(nameof({entityName}), nameof({entityName}.ID{entityName}), TransactionDB: TransactionDB);");
        boContent.AppendLine("            }");
        boContent.AppendLine();
        boContent.AppendLine($"            await {daoVariableName}.SaveChangesAsync(entity);");
        boContent.AppendLine("            return true;");
        boContent.AppendLine("        }");
        boContent.AppendLine("    }");
        boContent.AppendLine("}");

        string tempPath = Path.Combine(Path.GetTempPath(), $"{boClassName}.cs");
        File.WriteAllText(tempPath, boContent.ToString(), Encoding.UTF8);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Arquivo BO gerado com sucesso: {tempPath}");
        Console.ResetColor();

        Process.Start("notepad.exe", tempPath);
    }
    

}
