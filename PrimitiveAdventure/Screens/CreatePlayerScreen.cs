using PrimitiveAdventure.Core;
using PrimitiveAdventure.Core.Global;
using PrimitiveAdventure.SadConsole.Controls;
using PrimitiveAdventure.Screens.Base;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Screens;

public class CreatePlayerScreen: GlobalScreen
{
    private readonly TextBox _textBox;
    
    
    public CreatePlayerScreen(int slotIndex)
    {
        _textBox = new TextBox(10);
        var button = new Button("Начать".Prepare());
        button.Click += ButtonOnClick;
        
        Controls.GetControlCursor()
            .ToStart()
            .NewLine()
            .Print(new Label("Введите имя персонажа".Prepare()))
            .Print(new Label("Имя: ".Prepare()), _textBox)
            .Print(button);
    }

    private void ButtonOnClick(object? sender, EventArgs e)
    {
        if (_textBox.Text.Length > 0)
            new GameState(GlobalMap.GenerateMap(), new Player()).StartScreen();
    }
}