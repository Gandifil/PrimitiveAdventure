﻿using SadConsole.Input;
using SadConsole.UI;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.SadConsole.Screens;

/// <summary>
/// Copied from examples of SadConsole. 
/// </summary>
public class ScrollableConsole : ControlsConsole
{
    private readonly ScrollBar _scrollBar;
    private int _scrollOffset;
    private int _lastCursorY;
    private bool _allowInput;

    /// <summary>
    /// The child console that displays the text written by the cursor.
    /// </summary>
    public Console MessageBuffer { get; }

    /// <summary>
    /// When <see langword="true"/>, the console scrolls to the end when the cursor writes a new line.
    /// </summary>
    public bool AutomaticScroll { get; set; }

    /// <summary>
    /// When <see langword="true"/>, allow keyboard input to the cursor.
    /// </summary>
    public bool AllowInput
    {
        get => _allowInput;
        set
        {
            _allowInput = value;
            UseKeyboard = value;
            MessageBuffer.UseKeyboard = value;
        }
    }
    
    public ScrollableConsole(int width, int height, int maxLines) : base(width, height)
    {
        // Input settings
        UseKeyboard = false;
        UseMouse = true;
        FocusOnMouseClick = false;
        
        // Create the message buffer console
        MessageBuffer = new Console(Width - 1, Height, Width - 1, maxLines);
        MessageBuffer.UseMouse = false;
        MessageBuffer.UseKeyboard = false;

        // Reassign the message buffer cursor to this object
        SadComponents.Remove(Cursor);
        Cursor = MessageBuffer.Cursor;
        Cursor.IsVisible = false;
        Cursor.IsEnabled = true;

        // Remove the surface renderer, we don't care what this surface has on itself
        //Renderer!.Steps.RemoveAll(p => p.Name == Renderers.Constants.RenderStepNames.Surface);
        //Renderer.Steps.RemoveAll(p => p.Name == Renderers.Constants.RenderStepNames.Tint);
        
        // Handle the scroll bar control
        _scrollBar = new ScrollBar(Orientation.Vertical, Height);
        _scrollBar.IsEnabled = false;
        _scrollBar.ValueChanged += (sender, e) => MessageBuffer.ViewPosition = (0, _scrollBar.Value);
        _scrollBar.Position = (Width - 1, 0);
        Controls.Add(_scrollBar);

        Children.Add(MessageBuffer);
    }

    public void Clear()
    {
        MessageBuffer.Clear();
        _scrollOffset = 0;
        Cursor.Position = (0, 0);
        _scrollBar.IsEnabled = false;
    }

    public override void Update(TimeSpan delta)
    {
        // If cursor has moved below the visible area, track the difference
        if (MessageBuffer.Cursor.Position.Y > _scrollOffset + MessageBuffer.ViewHeight - 1)
            _scrollOffset = MessageBuffer.Cursor.Position.Y - MessageBuffer.ViewHeight + 1;

        // Adjust the scroll bar
        _scrollBar.IsEnabled = _scrollOffset != 0;
        _scrollBar.MaximumValue = _scrollOffset;

        // If autoscrolling is enabled, scroll
        if (_scrollBar.IsEnabled && AutomaticScroll && _lastCursorY != MessageBuffer.Cursor.Position.Y)
        {
            _scrollBar.Value = _scrollBar.MaximumValue;
            _lastCursorY = MessageBuffer.Cursor.Position.Y;
        }

        // Update the base class which includes the controls
        base.Update(delta);
    }

    public override bool ProcessKeyboard(Keyboard keyboard)
    {
        if (AllowInput)
            return MessageBuffer.ProcessKeyboard(keyboard);

        return false;
    }

    public override bool ProcessMouse(MouseScreenObjectState state)
    {
        if (state.Mouse.ScrollWheelValueChange != 0)
            return _scrollBar.ProcessMouseWheel(state);

        return base.ProcessMouse(state);
    }
}
