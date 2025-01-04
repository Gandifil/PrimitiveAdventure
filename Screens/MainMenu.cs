using SadConsole.UI;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Screens;

public class MainMenu : ControlsConsole
{
    private readonly Button _button;
    private readonly TextBox _textBox;
 
    public MainMenu(int width = 30, int height = 30) 
        : base(width, height)
    {
        _button = new Button(width: 30, height: 1);
        _button.Position = new Point(10, 5);
        _button.Text = "Click me!";
        Controls.Add(_button);
 
        _textBox = new TextBox(width: 30);
        _textBox.Position = new Point(10, 7);
        Controls.Add(_textBox);
    }
}