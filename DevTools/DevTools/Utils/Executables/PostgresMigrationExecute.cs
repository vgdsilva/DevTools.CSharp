using DevTools.Utils.Classes;
using DevTools.Utils.exceptions;
using System.Diagnostics;

namespace DevTools.Utils.Executables;

public class PostgresMigrationExecute
{
    public static void Start(string migrationName, string DadosQueNaoSeraoQuestionados = "")
    {

        var scriptPath = Path.Combine(CloverPaths.PostgreSQLFolderPath, "migration.ps1");
        var projectDir = CloverPaths.PostgreSQLFolderPath; // ou outro caminho onde está o .csproj

        var psi = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = $"-ExecutionPolicy Bypass -File \"{scriptPath}\" -NomeDaMigracao \"{migrationName}\" -DadosQueNaoSeraoQuestionados \"{DadosQueNaoSeraoQuestionados}\"",
            WorkingDirectory = projectDir, // <- aqui você define a pasta onde o script será executado
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = false
        };

        using ( var proc = Process.Start(psi) )
        {
            string output = proc.StandardOutput.ReadToEnd();
            string error = proc.StandardError.ReadToEnd();
            proc.WaitForExit();

            Console.WriteLine(output);

            if ( !string.IsNullOrWhiteSpace(error) )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("⚠ Erro ao executar a migration:");
                Console.WriteLine(error);
                Console.ResetColor();
            }

            if ( proc.ExitCode != 0 )
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"⚠ O script foi finalizado com código de saída {proc.ExitCode}.");
                Console.ResetColor();
            }
        }

        Console.ReadLine();
    }
}
