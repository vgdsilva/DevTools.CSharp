using DevTools.Utils.Executables;
using DevTools.Utils.Helpers;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DevTools.Data.Context;
public class ContextoUtil
{
    public IConfigurationRoot Configuration { get; private set; }

    public ContextoUtil()
    {
        LoadAppSettings();
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
        string[] pastasObrigatorias = { "web", "api", "db", "regras", "CRMServices", "CRMMobileMaui", "CRMContext" };

        return pastasObrigatorias.All(pasta => Directory.Exists(Path.Combine(caminho, pasta)));
    }


    public string GetBranchPath()
    {
        string branchPath = Configuration?.GetSection("CloverCRM")?["CurrentBranch"];
        if ( string.IsNullOrWhiteSpace(branchPath) )
        {
            Console.WriteLine("A configuração 'CurrentBranch' não foi encontrada no appsettings.json.");
            return string.Empty;
        }
        return branchPath;
    }

    public void LoadCloverCRMAssemblys()
    {
        string branchPath = GetBranchPath();
        
        Console.WriteLine($"Carregando assemblies da branch {branchPath}...");

        Console.WriteLine("Carregando assemblys do projeto CRM.Entidades...");

        string entidadesAssemblyPath = Path.Combine(branchPath, "regras", "CRM.Comum", "CRM.Entidades", "bin", "Release", "netstandard2.0", "CRM.Entidades.dll");
        if ( !File.Exists(entidadesAssemblyPath) )
            DotnetBuildExecutable.Execute(Path.Combine(branchPath, "regras", "CRM.Comum", "CRM.Entidades", "CRM.Entidades.csproj"));
        Assembly.LoadFrom(entidadesAssemblyPath);


        Console.WriteLine("Carregando assemblys do projeto CRMSync...");

        string crmSyncAssemblyPath = Path.Combine(branchPath, "db", "CRMSync", "CRMSync.Web.Api", "bin", "Release", "netcoreapp2.2", "CRMSync.Web.Api.dll");
        if ( !File.Exists(crmSyncAssemblyPath) )
            DotnetBuildExecutable.Execute(Path.Combine(branchPath, "db", "CRMSync", "CRMSync.Web.Api", "CRMSync.Web.Api.csproj"));
        Assembly.LoadFrom(crmSyncAssemblyPath);




    }

    public bool BuilderAndLoadAssemblys(string branchPath, string projectName)
    {



        return true;
    }

    private void LoadAppSettings()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        Configuration = builder.Build();
    }

    public void SalvarBranch(string novaBranch)
    {
        AppSettingsHelper.SaveAppSettings("CloverCRM:CurrentBranch", novaBranch);
        Console.WriteLine($"A branch do CloverCRM foi atualizada para: {novaBranch}");
        LoadAppSettings();
    }
}
