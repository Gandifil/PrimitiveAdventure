﻿namespace PrimitiveAdventure.Screens;

public abstract class BaseScreen: Console
{
    protected BaseScreen(int width, int height) : base(width, height)
    {
        Cursor.PrintAppearanceMatchesHost = false;
    }

    protected void Start(AnimatedScreenObject animation)
    {
        Children.Add(animation);
        animation.Start();
    }
}