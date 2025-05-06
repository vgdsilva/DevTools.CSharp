using DevTools.Data;
using DevTools.Data.Context;
using DevTools.Model;

namespace DevTools.Views;

public class MainView
{
    public static void Show()
    {
        Console.Clear();
        DisplayMainHeader();
        DisplayCurrentBranch();
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

    public static void DisplayCurrentBranch()
    {
        Console.WriteLine($"[!] Branch atual: {Contexto.Instance.CurrentBranch}");
    }

    public static void DisplayAvailableArguments()
    {
        Console.WriteLine();
        Console.WriteLine("Argumentos disponíveis:");

        // 1. Calcular o tamanho do alinhamento do primeiro alias
        int alignCol = MockData.MainOptions.Max(c => c.Aliases.Length == 1 ? 0 : c.Aliases[0].Length) + 2;

        // 2. Gerar todos os textos de alias formatados
        var aliasFormatados = MockData.MainOptions
            .Select(c => c.GetAliasFormatted(alignCol))
            .ToList();

        // 3. Calcular o comprimento máximo da parte do aliasFormatado (para alinhar a descrição)
        int maxAliasLength = aliasFormatados.Max(a => a.Length) + 4; // Espaço entre alias e descrição

        for ( int i = 0; i < MockData.MainOptions.Count; i++ )
        {
            var alias = aliasFormatados[i];
            var descricao = MockData.MainOptions[i].Descricao;
            string padding = new string(' ', maxAliasLength - alias.Length);

            Console.WriteLine($"       {alias} {padding} - {descricao}");
        }

        Console.WriteLine();
        Console.WriteLine("Exemplo de uso: dotnet run <nome do comando> <argumento para o comando> <...>");
        Console.WriteLine();
    }
}
