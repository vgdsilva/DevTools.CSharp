using DevTools.Data;
using DevTools.Model;
using DevTools.Utils.exceptions;
using DevTools.Views;

namespace DevTools;

internal class Program
{
    static void Main(string[] args)
    {
        AppConfiguration.Init();

        if (AppConfiguration.Instance["CurrentBranch"] == null)
        {
            Console.Clear();
            MainView.DisplayMainHeader();
            AppConfigurationUtils.ValidarOuSolicitarBranch(AppConfiguration.Instance);
        }

        bool Sair = false;

        while ( !Sair )
        {
            Console.Clear();
            MainView.DisplayMainHeader();
            MainView.DisplayConfigurations();

            if ( args.Length == 0 )
            {
                MainView.ExibirModoDeUso();
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
                        MainView.ExibirModoDeUso();
                    }
                }
                catch ( ExitException )
                {
                    Console.Title = "DevTools";
                    Console.Clear();
                    MainView.DisplayMainHeader();
                    MainView.ExibirModoDeUso();
                }
                catch ( Exception e )
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ERRO: {e}");
                    Console.ResetColor();
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

    


}
