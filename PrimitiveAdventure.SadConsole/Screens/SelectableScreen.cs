using SadConsole.Input;
using SadConsole.UI;

namespace PrimitiveAdventure.SadConsole.Screens;

public class SelectableScreen: BaseScreen
{
    public bool CanBeSelected { get; set; }
    
    public bool IsSelected { get; private set; }
    
    public Colors? SelectedColors { get; set; }

    private Colors? _oldColors;
    
    public SelectableScreen(int width, int height) : base(width, height)
    {
        MouseEnter += OnMouseEnter;
        MouseExit += OnMouseExit;
        
        SelectedColors = Colors.Default.Clone();
        SelectedColors.ControlForegroundNormal.SetColor(Color.Yellow);
        SelectedColors.ControlForegroundSelected.SetColor(Color.Yellow);
        SelectedColors.RebuildAppearances();
    }

    private void OnMouseExit(object? sender, MouseScreenObjectState e)
    {
        Controls.ThemeColors = _oldColors;
        IsSelected = false;
    }

    private void OnMouseEnter(object? sender, MouseScreenObjectState e)
    {
        if (CanBeSelected)
        {
            IsSelected = true;
            _oldColors = Controls.ThemeColors;
            Controls.ThemeColors = SelectedColors;
        }
    }
}