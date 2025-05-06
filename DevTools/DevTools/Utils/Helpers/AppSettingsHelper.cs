using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Utils.Helpers;
internal class AppSettingsHelper
{
    /// <summary>
    /// Salva uma nova seção ou atualiza um valor existente no appsettings.json.
    /// </summary>
    /// <param name="key">A chave da seção ou valor a ser salvo (ex: "CloverCRM:CurrentBranch").</param>
    /// <param name="value">O valor a ser salvo.</param>
    public static void SaveAppSettings(string key, string value)
    {
        try
        {
            string appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            string json = File.ReadAllText(appSettingsPath);
            dynamic jsonObj = JsonConvert.DeserializeObject(json)!;

            // Navega pelas seções da chave
            string[] keys = key.Split(':');
            dynamic currentLevel = jsonObj;
            for ( int i = 0; i < keys.Length - 1; i++ )
            {
                string sectionKey = keys[i];
                if ( currentLevel[sectionKey] == null )
                {
                    currentLevel[sectionKey] = new Newtonsoft.Json.Linq.JObject();
                }
                currentLevel = currentLevel[sectionKey];
            }

            // Define o valor na chave final
            currentLevel[keys.Last()] = value;

            string updatedJson = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText(appSettingsPath, updatedJson);

            // Opcional: Recarregar a configuração se estiver usando IConfiguration
            // ReloadConfiguration();
        }
        catch ( Exception ex )
        {
            Console.WriteLine($"Erro ao salvar no appsettings.json (chave: {key}, valor: {value}): {ex.Message}");
        }
    }

    /// <summary>
    /// (Opcional) Recarrega a configuração do appsettings.json se você estiver usando IConfiguration.
    /// </summary>
    // private static void ReloadConfiguration()
    // {
    //     var builder = new ConfigurationBuilder()
    //         .SetBasePath(Directory.GetCurrentDirectory())
    //         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    //     ConfigurationRoot = builder.Build();
    // }

    // public static IConfigurationRoot? ConfigurationRoot { get; private set; }
}
