using DevTools.Data;
using DevTools.Model;

namespace DevTools.Views;

public class MainView
{
    public static void Show()
    {
        Console.Clear();
        DisplayMainHeader();
        DisplayConfigurations();
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

    public static void DisplayConfigurations()
    {
        AppConfiguration config = new AppConfiguration();
        Console.WriteLine($"[!] Branch atual: {config["CurrentBranch"]}");
    }

    public static void ExibirModoDeUso()
    {
        Console.WriteLine();
        Console.WriteLine("Argumentos disponíveis:");

        // 1. Calcular o tamanho do alinhamento do primeiro alias
        int alignCol = MockData.MainMenuOptions.Max(c => c.Aliases.Length == 1 ? 0 : c.Aliases[0].Length) + 2;

        // 2. Gerar todos os textos de alias formatados
        var aliasFormatados = MockData.MainMenuOptions
            .Select(c => c.GetAliasFormatted(alignCol))
            .ToList();

        // 3. Calcular o comprimento máximo da parte do aliasFormatado (para alinhar a descrição)
        int maxAliasLength = aliasFormatados.Max(a => a.Length) + 4; // Espaço entre alias e descrição

        for ( int i = 0; i < MockData.MainMenuOptions.Count; i++ )
        {
            var alias = aliasFormatados[i];
            var descricao = MockData.MainMenuOptions[i].Descricao;
            string padding = new string(' ', maxAliasLength - alias.Length);

            Console.WriteLine($"             {alias}{padding}-{descricao}");
        }

        Console.WriteLine();
        Console.WriteLine("Exemplo de uso: dotnet run <nome do comando> <argumento para o comando> <...>");
        Console.WriteLine();
    }
}
