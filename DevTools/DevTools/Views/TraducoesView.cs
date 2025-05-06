using DevTools.Utils.exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Views;

public class TraducoesView
{
    public static void Show()
    {
        string plataform = RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? "macos" : "windows";

        // Caminho para o projeto MAUI Windows
        var mauiProjectPath = Path.Combine(AppContext.BaseDirectory, "Traducoes", plataform, "DevTools.TraducoesMAUI.exe");

        // Inicia o app MAUI
        Process.Start(new ProcessStartInfo
        {
            FileName = mauiProjectPath,
            UseShellExecute = true
        });


        using (Process mauiProcessStart = Process.Start(mauiProjectPath))
        {
            mauiProcessStart.WaitForInputIdle();
        }

        Console.WriteLine("[ !] Executando ferramenta de traduções");
        Thread.Sleep(2000);

        throw new ExitException();
    }
}
