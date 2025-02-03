namespace PrimitiveAdventure;

public static class StringExtensions
{
    public static string Prepare(this string str)
    {
        char transform(char c)
        {
            c = char.ToLower(c);
            if (c == 'ё') 
                c = 'е';
            var k = c - 'а';
            return (char)(128 + k);
        }
        return new string(str.Select(x => char.IsAscii(x) ? x : transform(x)).ToArray());
    }
}