using SadConsole.Input;

namespace PrimitiveAdventure.SadConsole.Screens;

public class ClickableScreen: SelectableScreen
{
    public bool CanClick { get; set; }
    
    public event EventHandler Click;
    
    public ClickableScreen(int width, int height) : base(width, height)
    {
        MouseButtonClicked += OnMouseButtonClicked;
    }

    private void OnMouseButtonClicked(object? sender, MouseScreenObjectState e)
    {
        if (CanClick)
            Click?.Invoke(this, EventArgs.Empty);
    }
}