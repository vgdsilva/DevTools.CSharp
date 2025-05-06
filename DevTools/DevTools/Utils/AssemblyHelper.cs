using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Utils;

public class AssemblyHelper
{

    public static Assembly BuildAndLoadAssembly(string csprojPath, string dllPath)
    {

        var startInfoDotnetBuild = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"build \"{csprojPath}\" --configuration Debug",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        

        using (Process dotnetBuildProcess = Process.Start(startInfoDotnetBuild))
        {
            string output = dotnetBuildProcess.StandardOutput.ReadToEnd();
            string error = dotnetBuildProcess.StandardError.ReadToEnd();
            dotnetBuildProcess.WaitForExit();

            if ( dotnetBuildProcess.ExitCode != 0 )
                throw new Exception($"Erro ao compilar o projeto: {error}");

            if ( !File.Exists(dllPath) )
                throw new FileNotFoundException($"Assembly não encontrado em: {dllPath}");

            return Assembly.LoadFrom(dllPath);
        }
    }
}
