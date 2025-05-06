using DevTools.Core.Context;
using DevTools.Data;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;

namespace DevTools.Core.Config;
public static class ConfigurationSetup
{
    private static IConfigurationRoot? _configurationRoot;
    private static CloverCRMContext? _cloverCRMContext;

    public static IConfigurationRoot Init()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables();

        _configurationRoot = builder.Build();
        return _configurationRoot;
    }

    public static CloverCRMContext GetCloverCRMContext()
    {
        if ( _cloverCRMContext == null )
        {
            string? currentBranch = _configurationRoot?.GetSection("CloverCRM")?["CurrentBranch"];
            string? entityProjectPath = _configurationRoot?.GetSection("CloverCRM")?["EntityProjectPath"];
            string? entityOutputDirectory = _configurationRoot?.GetSection("CloverCRM")?["EntityOutputDirectory"];

            if ( string.IsNullOrEmpty(currentBranch) )
            {
                Console.Clear();
                MainView.DisplayMainHeader();
                currentBranch = ConfigurationUtils.SolicitarBranch();
                SaveBranchToAppSettings(currentBranch);
            }

            if ( string.IsNullOrEmpty(entityProjectPath) || string.IsNullOrEmpty(entityOutputDirectory) )
            {
                Console.WriteLine("Caminho para o projeto de entidades ou diretório de saída não configurado no appsettings.json.");
                // Você pode adicionar lógica aqui para solicitar esses caminhos ao usuário
                // ou usar um caminho padrão, se apropriado.
                entityProjectPath = ""; // Defina um valor padrão ou solicite ao usuário
                entityOutputDirectory = ""; // Defina um valor padrão ou solicite ao usuário
            }

            _cloverCRMContext = new CloverCRMContext(currentBranch!, entityProjectPath!, entityOutputDirectory!);
        }
        return _cloverCRMContext;
    }
}
