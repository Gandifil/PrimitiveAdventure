using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.Screens.Base;
using PrimitiveAdventure.Screens.Views;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Screens;

public class MainMenu: GlobalScreen
{
    private readonly Button _button;
    private readonly TextBox _textBox;
 
    public MainMenu()
    {
        _button = new Button("Click me2!");
        _button.Position = new Point(10, 5);
        //_button.Text = "Click me!";
        Controls.Add(_button);
        
        _button = new Button("Об игре".Prepare());
        _button.Position = new Point(10, 6);
        //_button.Text = "Click me!";
        _button.Click += (_, __) =>
        {
            new BackScreen<Credits>((_, _) => this.Start()).Start();
        };
        Controls.Add(_button);
        
        _button = new Button("Exit");
        _button.Position = new Point(11, 7);
        //_button.Text = "Click me!";
        _button.Click += (_, __) => Environment.Exit(0);
        Controls.Add(_button);
 
        _textBox = new TextBox(width: 30);
        _textBox.Position = new Point(10, 8);
        Controls.Add(_textBox);
    }
}