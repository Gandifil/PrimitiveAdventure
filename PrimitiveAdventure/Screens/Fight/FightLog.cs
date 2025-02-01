using PrimitiveAdventure.SadConsole.Screens;

namespace PrimitiveAdventure.Screens.Fight;

public class FightLog: ScrollableConsole
{
    public static FightLog Instance { get; private set; } = null!;
    public FightLog(int width, int height) : base(width, height, 10000)
    {
        Instance = this;
        Cursor.UseStringParser = true;
    }

    public void PrintLine(string text)
    {
        text = "[c:r f:AnsiWhite]   " + text.Prepare();
        MessageBuffer.Cursor.Print(text).NewLine();
    }
}