using System.Text;

namespace PrimitiveAdventure.SadConsole;

public class ColoredStringBuilder
{
    private readonly StringBuilder _stringBuilder = new ();

    public void AppendWithColor(Color color, string text)
    {
        _stringBuilder
            .Append("[c:r f:")
            .Append(color)
            .Append("]")
            .Append(text)
            .Append("[c:u 1]");
    }

    public void Append(string text)
    {
        
    }

    public static string Name(string text) => "[c:r f:Yellow]" + text + "[c:u 1]";
    public static string Damage(string text) => "[c:r f:Red]" + text + "[c:u 1]";
    public static string Positive(string text) => "[c:r f:LightGreen]" + text + "[c:u 1]";
}