using DevTools.Data;
using DevTools.Model;
using DevTools.Utils.exceptions;

namespace DevTools;

internal class Program
{
    static AppConfiguration _appConfiguration;

    static void Main(string[] args)
    {
        _appConfiguration = new AppConfiguration();
        AppConfigurationUtils.ValidarOuSolicitarBranch(_appConfiguration);

        bool Sair = false;

        while ( !Sair )
        {
            Console.Clear();
            AppConfigurationUtils.DisplayMainHeader();

            if ( args.Length == 0 )
            {
                ExibirModoDeUso();
            }
            else
            {
                try
                {
                    string comando = args[0];
                    var menuOption = MockData.MainMenuOptions.FirstOrDefault(o => o.Aliases.Contains(comando));

                    if (menuOption is not null)
                        menuOption.Acao(args);
                    else
                    {
                        Console.WriteLine("Argumento inválido: '" + comando + "'");
                        ExibirModoDeUso();
                    }
                }
                catch ( ExitException )
                {
                    Console.Title = "DevTools";
                    Console.Clear();
                    AppConfigurationUtils.DisplayMainHeader();
                    ExibirModoDeUso();
                }
                catch ( Exception e )
                {
                    Console.Error.WriteLine($"ERRO: {e}");
                    throw;
                }
            }

            if ( !Sair )
            {
                Console.Write("Entre com um argumento válido ('sair' para encerrar): ");
                args = Console.ReadLine()?.Split(' ') ?? Array.Empty<string>();
            }
        }
    }

    static void ExibirModoDeUso()
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
