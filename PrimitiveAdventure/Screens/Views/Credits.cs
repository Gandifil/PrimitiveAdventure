﻿using System.Reflection;
using SadConsole.Instructions;

namespace PrimitiveAdventure.Screens.Views;

public class Credits: Console
{
    public Credits() : base(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT-1)
    {
        string[] text =
        {
            "Name : Primitive Adventure",
            string.Empty,
            $"Version : {Assembly.GetExecutingAssembly().GetName().Version}",
            string.Empty,
            "Made by : gandifil, gandifilmk@gmail.com",
            string.Empty,
            "Made for www.igdc.ru"
        };
        var typingInstruction = new DrawString(ColoredString.Parser.Parse(string.Join("\r\n", text)));
        typingInstruction.TotalTimeToPrint = TimeSpan.FromSeconds(4); 

        Cursor.Position = new Point(1, 1);
        Cursor.IsEnabled = false;
        Cursor.IsVisible = true;
        typingInstruction.Cursor = Cursor;

        SadComponents.Add(typingInstruction);
    }
}