using DevTools.Utils.exceptions;
using DevTools.Utils.Interfaces;
using DevTools.Utils.Generator;

namespace DevTools.Views;
public class CodeView
{
    /// args: <code|geradorDeCodigo> <nome do comando> <argumento para o comando>
    public static void Show(string[] args)
    {
        bool shouldExit = false;
        bool isFirstExecution = true;

        var commands = InitializeCommands();

        while ( !shouldExit )
        {
            if ( isFirstExecution )
            {
                Console.Clear();
                DisplayHeader();
                isFirstExecution = false;
            }

            if ( args.Length < 2 )
                DisplayUsage();
            else
                ExecuteCommand(args, commands, ref shouldExit);

            if ( !shouldExit )
                PromptForNextCommand(ref args);
            else
                throw new ExitException();
        }
    }

    // Initialize available commands
    private static Dictionary<string, Action<string[]>> InitializeCommands()
    {
        return new Dictionary<string, Action<string[]>>
        {
            ["e"] = GenerateEntity,
            ["entidade"] = GenerateEntity,
            ["bo"] = GenerateBO,
            ["dao"] = GenerateDAO,
            ["dto"] = GenerateDTO,
            ["ac"] = GenerateControllerApi,
            ["api-controller"] = GenerateControllerApi,
            ["voltar"] = args => Environment.Exit(0), // Exit on "voltar"
            ["v"] = args => Environment.Exit(0) // Alias for exit
        };
    }

    // Execute the specified command
    private static void ExecuteCommand(string[] args, Dictionary<string, Action<string[]>> commands, ref bool shouldExit)
    {
        string command = args[1];
        if ( commands.TryGetValue(command, out var action) )
        {
            try
            {
                action(args);
                if ( command != "voltar" && command != "v" )
                {
                    Console.WriteLine("\nProcesso concluído...");
                }
            }
            catch ( Exception ex )
            {
                DisplayError(ex.Message);
            }
            finally
            {
                if ( command != "voltar" && command != "v" )
                {
                    PauseAndDisplayUsage(args);
                }
            }
        }
        else
        {
            DisplayError($"Comando inválido: '{command}'");
            PauseAndDisplayUsage(args);
        }
    }

    // Pause and display usage again
    private static void PauseAndDisplayUsage(string[] args)
    {
        Thread.Sleep(500);
        Console.ReadLine();
        args = args.Take(args.Length - 1).ToArray();
        Console.Clear();
        DisplayHeader();
        DisplayUsage();
    }

    static void DisplayHeader()
    {
        Console.WriteLine(@"    ___              _____               _      ");
        Console.WriteLine(@"   /   \ ___ __   __/__   \ ___    ___  | | ___ ");
        Console.WriteLine(@"  / /\ // _ \\ \ / /  / /\// _ \  / _ \ | |/ __|");
        Console.WriteLine(@" / /_//|  __/ \ V /  / /  | (_) || (_) || |\__ \");
        Console.WriteLine(@"/___,'  \___|  \_/   \/    \___/  \___/ |_||___/");
        Console.WriteLine("               Aliare | o campo sem fronteiras_  ");
        Console.WriteLine();
        Console.WriteLine("[ !] Atenção! Você está executando a ferramenta de desenvolvimento para geração de código");
        Console.WriteLine();
    }

    static void DisplayUsage()
    {
        Console.WriteLine();
        Console.WriteLine("Argumentos disponíveis:");
        Console.WriteLine("            e|entidade       <nome(s) da(s) entidade(s)>           - Se possuir o caminho ira gerar todos os arquivos abaixo senão ira criar uma entidade do zero");
        Console.WriteLine("             |bo             <nome(s) da(s) entidade(s)>                      - Gera o arquivo BO básico a partir do nome da entidade");
        Console.WriteLine("             |dao            <nome(s) da(s) entidade(s)>                      - Gera o arquivo DAO básico a partir do nome da entidade");
        Console.WriteLine("             |dto            <nome(s) da(s) entidade(s)>     - Gera DTO a partir de um arquivo de entidade");
        Console.WriteLine("           ac|api-controller <nome(s) da(s) entidade(s)>                      - Gera o arquivo Controller básico a partir do nome da entidade");
        Console.WriteLine("            v|voltar                                                          - Encerra este prompt e volta para a tela principal");
        Console.WriteLine();
        Console.WriteLine("Exemplo de uso: dotnet run code <nome do comando> <argumento para o comando> <...>");
        Console.WriteLine();
    }

    private static void GerarEntidade(string[] args)
    {
        if ( args.Length < 3 )
            throw new Exception("Uso: dotnet run code <e|entidade> <caminho(s)|nome(s) da(s) entidade(s)>");



        // Dicionário para mapear as ações
        //Dictionary<string, Action<string[]>> process = new Dictionary<string, Action<string[]>>
        //{
        //    ["BO"] = _gerarBO ? GerarBO : (Action<string[]>) null,
        //    ["DAO"] = _gerarDAO ? GerarDAO : (Action<string[]>) null,
        //    ["DTO"] = _gerarDTO ? GerarDTO : (Action<string[]>) null,
        //    ["ApiController"] = _gerarControllerApi ? GerarControllerApi : (Action<string[]>) null
        //};

        //ProgressBar progressBar = new ProgressBar(process.Count);

        //// Executando as ações com base nas respostas
        //foreach ( var item in process )
        //{
        //    if (item.Value != null )
        //    {
        //        Thread.Sleep(500);
        //        Console.WriteLine();

        //        Console.WriteLine($"Gerando {item.Key}...");
        //        item.Value(args);
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Não será gerado {item.Key}.");
        //    }
        //}
    }

    /// <summary>
    /// Prompt for the next valid command
    /// </summary>
    /// <param name="args"></param>
    private static void PromptForNextCommand(ref string[] args)
    {
        Console.Write("Entre com um argumento válido ('voltar' para voltar): ");
        string? input = Console.ReadLine();
        string param1 = args[0];
        string[] additionalArgs = input?.Split(' ') ?? Array.Empty<string>();
        args = new string[] { param1 }.Concat(additionalArgs).ToArray();
    }

    /// <summary>
    /// Display error message in red
    /// </summary>
    /// <param name="message"></param>
    private static void DisplayError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"ERRO: {message}");
        Console.ResetColor();
    }

    /// <summary>
    /// Actions for generating files
    /// </summary>
    /// <param name="args"></param>
    private static void GenerateEntity(string[] args) => GenerateFiles(args, new EntityGenerator());
    private static void GenerateBO(string[] args) => GenerateFiles(args, new BOGenerator());
    private static void GenerateDAO(string[] args) => GenerateFiles(args, new DAOGenerator());
    private static void GenerateDTO(string[] args) => GenerateFiles(args, new DTOGenerator());
    private static void GenerateControllerApi(string[] args) => GenerateFiles(args, new ApiControllerGenerator());

    /// <summary>
    /// Common file generation logic
    /// </summary>
    /// <param name="args"></param>
    /// <param name="generator"></param>
    /// <exception cref="Exception"></exception>
    private static void GenerateFiles(string[] args, IFileGenerator generator)
    {
        if ( args.Length < 3 )
            throw new Exception($"Uso: dotnet run code {args[0]} <nome(s) da(s) entidade(s)>");

        for ( int i = 2; i < args.Length; i++ )
        {
            generator.Generate(args[i]);
        }
    }
}
