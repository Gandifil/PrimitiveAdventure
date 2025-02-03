using SadConsole.Components;

namespace PrimitiveAdventure.SadConsole;

public static class CursorExtensions
{
    public static Cursor Tab(this Cursor cursor, int size = 4)
    {
        //cursor.Position += (size, 0); TODO: don't work, cause a bug
        //cursor.Column += size;
        return cursor;
    }
    
    public static Cursor PrintText(this Cursor cursor, string text)
    {
        foreach (var line in text.Split('\n'))
            cursor.Tab().Print(line).NewLine().NewLine();
        return cursor;
    }
}