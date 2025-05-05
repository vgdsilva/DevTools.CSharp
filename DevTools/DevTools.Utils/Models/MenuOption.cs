namespace DevTools.Utils.Models;

public class MenuOption
{
    public string[] Aliases { get; }
    public string Descricao { get; }
    public Action<string[]> Acao { get; }

    public MenuOption(string[] aliases, string descricao, Action<string[]> acao)
    {
        Aliases = aliases;
        Descricao = descricao;
        Acao = acao;
    }

    public string GetAliasFormatted(int alignCol)
    {
        string left = ""; // Antes do '|'
        string right = ""; // Depois do '|'

        if ( Aliases.Length == 1 )
        {
            right = Aliases[0];
        }
        else
        {
            left = Aliases[0];
            right = string.Join("|", Aliases.Skip(1));
        }

        int padding = alignCol - left.Length;
        if ( padding < 0 ) padding = 0;

        return $"{new string(' ', padding)}{left}|{right}";
    }
}
