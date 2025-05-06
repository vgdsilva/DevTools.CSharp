using DevTools.Data;
using DevTools.Data.Context;
using DevTools.Utils.exceptions;
using DevTools.Views;

namespace DevTools;

internal class Program
{
    static void Main(string[] args)
    {
        Contexto.AssignNewInstance();

        bool Sair = false;

        while ( !Sair )
        {
            Console.Clear();
            MainView.DisplayMainHeader();
            MainView.DisplayCurrentBranch();

            if ( args.Length == 0 )
            {
                MainView.DisplayAvailableArguments();
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
                        MainView.DisplayAvailableArguments();
                    }
                }
                catch ( ExitException )
                {
                    Console.Title = "DevTools";
                    Console.Clear();
                    MainView.DisplayMainHeader();
                    MainView.DisplayAvailableArguments();
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
