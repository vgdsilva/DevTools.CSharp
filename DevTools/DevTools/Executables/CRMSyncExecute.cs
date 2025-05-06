using DevTools.Utils;
using DevTools.Utils.exceptions;
using System.Diagnostics;

namespace DevTools.Executables;

public static class CRMSyncExecute
{
    public static void Start()
    {
        Console.Write("Iniciando sincronizador");

        // Inicia processo em nova janela do terminal
        var psi = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/c start dotnet run --project {CloverPaths.CRMSyncProjectPath} -c Release",
            UseShellExecute = true, // IMPORTANTE para abrir nova janela
            CreateNoWindow = false
        };

        // Inicia o processo
        Process proc = Process.Start(psi);

        // Animação de carregamento simples
        for ( int i = 0; i < 5; i++ )
        {
            Thread.Sleep(500);
            Console.Write(".");
        }

        throw new ExitException();
    }
}
