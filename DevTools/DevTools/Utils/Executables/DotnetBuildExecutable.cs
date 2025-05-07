using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Utils.Executables;

public class DotnetBuildExecutable
{
    public static void Execute(string projectPath, string configuration = "Release")
    {
        string command = $"dotnet build {projectPath} -c {configuration}";
        //Console.WriteLine($"Executing command: {command}");
        
        var processInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe", "/c " + command)
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (var process = System.Diagnostics.Process.Start(processInfo))
        {
            process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
            process.ErrorDataReceived += (sender, e) => Console.WriteLine(e.Data);
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }
    }
}
