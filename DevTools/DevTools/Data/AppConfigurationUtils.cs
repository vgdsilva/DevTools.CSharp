using DevTools.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Data;

public class AppConfigurationUtils
{
    public static void ValidarOuSolicitarBranch(AppConfiguration config)
    {
        Console.Clear();
        DisplayMainHeader();

        var currentBranch = config["CurrentBranch"];

        while ( string.IsNullOrWhiteSpace(currentBranch) || !BranchValida(currentBranch) )
        {
            Console.WriteLine("[!] Configuração 'CurrentBranch' não encontrada ou inválida.");
            Console.Write("Informe o caminho da branch (ex: C:\\Projetos\\MinhaBranch): ");
            currentBranch = Console.ReadLine()?.Trim();

            if ( string.IsNullOrWhiteSpace(currentBranch) )
            {
                Console.WriteLine("Caminho inválido. Tente novamente.\n");
                continue;
            }

            if ( !BranchValida(currentBranch) )
            {
                Console.WriteLine("Branch inválida. As seguintes pastas são obrigatórias:");
                Console.WriteLine("- web\n- api\n- db\n- regras\n- CRMServices\n- CRMMobileMaui\n- CRMContext\n");
                continue;
            }

            config["CurrentBranch"] = currentBranch;
            config.Salvar();
            Console.WriteLine($"Branch '{currentBranch}' definida como 'CurrentBranch'.\n");
            Thread.Sleep(2000);
        }
    }

    static bool BranchValida(string caminho)
    {
        string[] pastasObrigatorias = {
        "web", "api", "db", "regras",
        "CRMServices", "CRMMobileMaui", "CRMContext"
    };

        return pastasObrigatorias.All(pasta => Directory.Exists(Path.Combine(caminho, pasta)));
    }

    public static void DisplayMainHeader()
    {
        Console.WriteLine(@"    ___              _____               _      ");
        Console.WriteLine(@"   /   \ ___ __   __/__   \ ___    ___  | | ___ ");
        Console.WriteLine(@"  / /\ // _ \\ \ / /  / /\// _ \  / _ \ | |/ __|");
        Console.WriteLine(@" / /_//|  __/ \ V /  / /  | (_) || (_) || |\__ \");
        Console.WriteLine(@"/___,'  \___|  \_/   \/    \___/  \___/ |_||___/");
        Console.WriteLine("               aliare | o campo sem fronteiras_  ");
        Console.WriteLine();
        Console.WriteLine("[ !] Atenção! Você está executando a ferramenta de desenvolvimento");
        Console.WriteLine();
    }
}
