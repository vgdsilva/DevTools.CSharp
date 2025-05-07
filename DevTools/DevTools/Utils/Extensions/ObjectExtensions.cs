namespace DevTools;

public static class ObjectExtensions
{
    public static string ToCamelCase(this string str)
    {
        if ( string.IsNullOrEmpty(str) )
            return str;

        return char.ToUpper(str[0]) + str.Substring(1);
    }
}
