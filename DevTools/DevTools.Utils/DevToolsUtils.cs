using DevTools.Utils.Models;

namespace DevTools;

public class DevToolsUtils
{
    public static void WriteMenu(List<MenuOption> options, MenuOption selectecOption)
    {

    }
}

public static class Extensions
{
    public static string ToCamelCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        return char.ToUpper(str[0]) + str.Substring(1);
    }
}
