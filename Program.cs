using CRM.CodeGenerator.Services;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace CRM.CodeGenerator
{
    internal class Program
    {

        static void Main(string[] args)
        {
            bool Sair = false;

            while ( !Sair )
            {
                Console.Clear();
                WriteLine(@"   _____ _____  __  __   _____          _       _____                           _             ");
                WriteLine(@"  / ____|  __ \|  \/  | / ____|        | |     / ____|                         | |            ");
                WriteLine(@" | |    | |__) | \  / || |     ___   __| | ___| |  __  ___ _ __   ___ _ __ __ _| |_ ___  _ __ ");
                WriteLine(@" | |    |  _  /| |\/| || |    / _ \ / _` |/ _ \ | |_ |/ _ \ '_ \ / _ \ '__/ _` | __/ _ \| '__|");
                WriteLine(@" | |____| | \ \| |  | || |___| (_) | (_| |  __/ |__| |  __/ | | |  __/ | | (_| | || (_) | |   ");
                WriteLine(@"  \_____|_|  \_\_|  |_(_)_____\___/ \__,_|\___|\_____|\___|_| |_|\___|_|  \__,_|\__\___/|_|   ");
                WriteLine(@"                                                          aliare | o campo sem fronteiras_    ");
                Console.WriteLine();
                WriteLine("[ !] Atenção! Você está executando a ferramenta de geração de codigo!");
                Console.WriteLine();

                if ( args.Length > 0 )
                    ExecutarModosDeUso(ref args, ref Sair);

                if ( !Sair )
                {
                    ExibirModoDeUso();
                    Write("Entre com um argumento válido ('sair' para encerrar): ");
                    args = Console.ReadLine()?.Split(' ');
                }
            }
        }

        static void WriteLine(string? value, TypeWrite typeWrite = TypeWrite.Normal)
        {
            Console.ForegroundColor = typeWrite switch
            {
                TypeWrite.Normal => ConsoleColor.White,
                TypeWrite.Warning => ConsoleColor.Yellow,
                TypeWrite.Error => ConsoleColor.Red
            };

            Console.WriteLine(value);

            Console.ResetColor();
        }
        
        static void Write(string? value, TypeWrite typeWrite = TypeWrite.Normal)
        {
            Console.ForegroundColor = typeWrite switch
            {
                TypeWrite.Normal => ConsoleColor.White,
                TypeWrite.Warning => ConsoleColor.Yellow,
                TypeWrite.Error => ConsoleColor.Red
            };

            Console.Write(value);

            Console.ResetColor();
        }

        enum TypeWrite
        {
            Normal,
            Warning,
            Error
        }

        static void ExibirModoDeUso()
        {
            Console.WriteLine();
            WriteLine("Argumentos disponíveis:");
            WriteLine("            entidade|gerarEntidade <caminho do arquivo | nome da entidade> - Se possuir o caminho ira gerar todos os arquivos abaixo senão ira criar uma entidade do zero");
            WriteLine("                  bo|gerarBO <nome da entidade>                    - Gera o arquivo BO básico a partir do nome da entidade");
            WriteLine("                 dao|gerarBO <nome da entidade>                    - Gera o arquivo DAO básico a partir do nome da entidade");
            WriteLine("                 dto|gerarDTO <caminho do arquivo>                 - Gera DTO a partir de um arquivo de entidade");
            WriteLine("       controllerApi|gerarControllerApi <nome da entidade>         - Gera o arquivo Controller básico a partir do nome da entidade");
            WriteLine("                sair|                                              - Encerra este prompt");
            Console.WriteLine();
            WriteLine("Exemplo de uso: dotnet run <nome do comando> <argumento para o comando>");
            Console.WriteLine();
        }

        static void ExecutarModosDeUso(ref string[] args, ref bool Sair)
        {
            try
            {
                switch ( args[0] )
                {
                    case "entidade" or "gerarEntidade":
                        if ( args.Length == 1 )
                            throw new Exception("Argumento inválido: 'entidade' ou 'gerarEntidade' precisa de um argumento");
                        
                        new EntityService().GenerateCode(args[1]);
                        break;
                    case "bo" or "gerarBO":
                        if ( args.Length == 1 )
                            throw new Exception("Argumento inválido: 'bo' ou 'gerarBO' precisa de um argumento");
                         
                        new BOService().GenerateCode(args[1]);
                        break;
                    case "dao" or "gerarDAO":
                        if ( args.Length == 1 )
                            throw new Exception("Argumento inválido: 'dao' ou 'gerarDAO' precisa de um argumento");
                            
                        new DAOService().GenerateCode(args[1]);
                        break;
                    case "dto" or "gerarDTO":
                        if ( args.Length == 1 )
                            throw new Exception("Argumento inválido: 'dto' ou 'gerarDTO' precisa de um argumento");

                        for ( int i = 1; i <= args.Length; i++ )
                            new DTOService().GenerateCode(args[i]);

                        Console.ReadLine();
                        break;
                    case "controllerApi" or "gerarControllerApi":
                        if ( args.Length == 1 )
                            throw new Exception("Argumento inválido: 'controllerApi' ou 'gerarControllerApi' precisa de um argumento");
                            
                        new ControllerApiService().GenerateCode(args[1]);
                        break;
                    case "sair":
                        Sair = true;
                        Environment.Exit(0);
                        break;
                    default:
                        WriteLine("Argumento inválido: '" + args[0] + "'");
                        ExibirModoDeUso();
                        break;
                }
            }
            catch ( NotImplementedException ex )
            {
                WriteLine("Metodo ainda não implementado.", TypeWrite.Error);
                Thread.Sleep(1000);
            }
            catch ( Exception ex )
            {
                WriteLine("Erro: " + ex.Message, TypeWrite.Error);
                Thread.Sleep(1000);
            }
        }

    }
}
