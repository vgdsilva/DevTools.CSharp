using DevTools.CodeGenerator.Interactive;
using DevTools.Executables;
using DevTools.Utils.exceptions;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DevTools.Views;
public class MigrationsView
{
    public static void Show()
    {
        Console.WriteLine();
        Console.WriteLine("[ !] Executando ferramenta de geração de migrations.");
        Console.WriteLine();

        bool isPostgreSQLMigration = InteractiveYesNoPrompt.Ask(
            "Para qual banco de dados deseja gerar a migration?",
            "PostgreSQL",
            "SQLite"
        );

        if (isPostgreSQLMigration)
        {
            Console.WriteLine();

            string nomeDaMigration = "";
            while ( string.IsNullOrWhiteSpace(nomeDaMigration) )
            {
                Console.Write("Informe o nome da migration: ");
                nomeDaMigration = Console.ReadLine()?.Trim();

                if ( string.IsNullOrWhiteSpace(nomeDaMigration) )
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("⚠ O nome da migration é obrigatório. Tente novamente.");
                    Console.ResetColor();
                }
            }

            Console.WriteLine();
            Console.WriteLine($"⏳ Gerando migration '{nomeDaMigration}' para PostgreSQL...");
            PostgresMigrationExecute.Start(nomeDaMigration);
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("⏳ Gerando migration para SQLite...");
            SQLiteMigrationExecute.Start();
        }

        Thread.Sleep(2000);

        throw new ExitException();
    }
}
