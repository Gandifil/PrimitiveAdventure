using System.Reflection;
using SadConsole.Instructions;

namespace PrimitiveAdventure.Screens.Views;

public class FailView: Console
{
    public FailView() : base(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT-1)
    {
        string[] text =
        {
            "Твоё тело пало, но дух ещё горит. Каждое поражение — это урок, высеченный в камне судьбы".Prepare()
        };
        
        var typingInstruction = new DrawString(ColoredString.Parser.Parse(string.Join("\r\n", text)));
        typingInstruction.TotalTimeToPrint = TimeSpan.FromSeconds(4); 

        Cursor.Position = new Point(1, 1);
        Cursor.IsEnabled = false;
        Cursor.IsVisible = true;
        Cursor.SetPrintAppearance(Color.Red);
        typingInstruction.Cursor = Cursor;

        SadComponents.Add(typingInstruction);
    }
}