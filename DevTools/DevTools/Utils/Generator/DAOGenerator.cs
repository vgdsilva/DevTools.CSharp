using DevTools.Utils.Interfaces;
using System.Diagnostics;
using System.Text;

namespace DevTools.Utils.Generator;

public class DAOGenerator : IFileGenerator
{
    public DAOGenerator()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Iniciando geração de DAO...");
        Console.ResetColor();
    }

    public void Generate(string entityName)
    {

        StringBuilder daoContent = new StringBuilder();

        daoContent.AppendLine("using CRM.DAO.interfaces;");
        daoContent.AppendLine("using CRM.Entidades.entidades;");
        daoContent.AppendLine();
        daoContent.AppendLine("namespace CRM.DAO.dao");
        daoContent.AppendLine("{");
        daoContent.AppendLine($"    public class {entityName}DAO : AbstractDAO<{entityName}>");
        daoContent.AppendLine("    {");
        daoContent.AppendLine($"        public {entityName}DAO(ITransactionDB TransactionDB = null, DCSequenceController Controller = null)");
        daoContent.AppendLine("            : base(TransactionDB, Controller) { }");
        daoContent.AppendLine("    }");
        daoContent.AppendLine("}");

        string tempPath = Path.Combine(Path.GetTempPath(), $"{entityName}DAO.cs");
        File.WriteAllText(tempPath, daoContent.ToString(), Encoding.UTF8);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Arquivo DAO gerado com sucesso: {tempPath}");
        Console.ResetColor();

        Process.Start("notepad.exe", tempPath);
    }
}
