﻿using PrimitiveAdventure.Ui;
using PrimitiveAdventure.Ui.Controls;
using SadConsole.Input;
using SadConsole.UI;

namespace PrimitiveAdventure.Screens;

public abstract class BaseScreen: ControlsConsole
{
    protected BaseScreen(int width, int height) : base(width, height)
    {
        Cursor.PrintAppearanceMatchesHost = false;
    }

    public void Start()
    {
        Game.Instance.Screen.IsFocused = false;
        Game.Instance.Screen = this;
        Game.Instance.Screen.IsFocused = true;
    }

    protected void StartAnimation(AnimatedScreenObject animation)
    {
        Children.Add(animation);
        animation.Start();
    }

    public override bool ProcessKeyboard(Keyboard keyboard)
    {
        foreach (var control in Controls)
            if (control is KeyedButton keyedButton)
                if (keyedButton.ForceProcessKeyboard(keyboard))
                    return true;

        return base.ProcessKeyboard(keyboard);
    }
}