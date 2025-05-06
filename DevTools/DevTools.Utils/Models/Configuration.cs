using System.Text.Json;
using System.Text.Json.Nodes;

namespace DevTools.Model;

public class Configuration
{
    private static Configuration? _instance;

    public static Configuration Instance
    {
        get
        {
            if ( _instance == null )
            {
                _instance = new Configuration();
            }
            return _instance;
        }
    }

    public static void Init()
    {
        _instance = new Configuration();
    }


    private static readonly string _caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
    private JsonObject _configJson;
    public string? this[string chave]
    {
        get => ObterValor(chave);
        set
        {
            if ( value is not null )
            {
                DefinirValor(chave, value);
            }
        }
    }

    public Configuration()
    {
        Carregar();
    }

    public void Carregar()
    {
        if ( File.Exists(_caminhoArquivo) )
        {
            var json = File.ReadAllText(_caminhoArquivo);
            _configJson = JsonNode.Parse(json)?.AsObject() ?? new JsonObject();
        }
        else
        {
            _configJson = new JsonObject();
            Salvar();
        }

        Console.Title = "DevTools";
    }


    public void Salvar()
    {
        var json = _configJson.ToJsonString(new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_caminhoArquivo, json);
    }

    private string? ObterValor(string chave)
    {
        var partes = chave.Split(':');
        JsonNode? atual = _configJson;

        foreach ( var parte in partes )
        {
            atual = atual?[parte];
            if ( atual == null ) return null;
        }

        return atual.ToString();
    }

    private void DefinirValor(string chave, string valor)
    {
        var partes = chave.Split(':');
        JsonObject atual = _configJson;

        for ( int i = 0; i < partes.Length - 1; i++ )
        {
            if ( atual[partes[i]] is not JsonObject prox )
            {
                prox = new JsonObject();
                atual[partes[i]] = prox;
            }

            atual = prox;
        }

        atual[partes[^1]] = valor;
    }

    // Método extra: retorna a seção como JsonObject
    public JsonObject? GetSection(string chave)
    {
        var partes = chave.Split(':');
        JsonNode? atual = _configJson;

        foreach ( var parte in partes )
        {
            atual = atual?[parte];
            if ( atual == null ) return null;
        }

        return atual as JsonObject;
    }
}
