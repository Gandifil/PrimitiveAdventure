﻿using PrimitiveAdventure.SadConsole.Controls;
using SadConsole.Input;

namespace PrimitiveAdventure.Screens.Base;

public class BackScreen<T>: GlobalScreen where T: ScreenObject, new()
{
    private readonly KeyedButton _exitButton;
    
    public BackScreen(EventHandler exitHandler)
    {
        Children.Add(new T());
        Controls.Add(_exitButton = new KeyedButton("назад".Prepare(), Keys.Escape));
        _exitButton.Position = new Point(1, Height - 1);
        _exitButton.Click += exitHandler;
    }
}