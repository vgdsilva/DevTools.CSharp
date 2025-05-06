using DevTools.Model;
using DevTools.Utils.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Data.Context;
public class ContextoUtil
{
    public IConfigurationRoot Configuration { get; private set; }

    public ContextoUtil()
    {
        Configuration = LoadAppSettings();
    }

    public string SolicitarBranch()
    {
        string currentBranch = string.Empty;

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
#if DEBUG
                Console.WriteLine("- web\n- api\n- db\n- regras\n- CRMServices\n- CRMMobileMaui\n- CRMContext\n");
#endif
                continue;
            }

            break;
        }

        return currentBranch;
    }

    bool BranchValida(string caminho)
    {
        string[] pastasObrigatorias = {
        "web", "api", "db", "regras",
        "CRMServices", "CRMMobileMaui", "CRMContext"
    };

        return pastasObrigatorias.All(pasta => Directory.Exists(Path.Combine(caminho, pasta)));
    }

    private IConfigurationRoot LoadAppSettings()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        return builder.Build();
    }

    public void SalvarBranch(string novaBranch)
    {
        AppSettingsHelper.SaveAppSettings("CloverCRM:CurrentBranch", novaBranch);
        Console.WriteLine($"A branch do CloverCRM foi atualizada para: {novaBranch}");
    }
}
