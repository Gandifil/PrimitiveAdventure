using PrimitiveAdventure.Core;
using PrimitiveAdventure.Core.Rpg.Items;
using PrimitiveAdventure.SadConsole.Controls;
using PrimitiveAdventure.Screens.Base;
using SadConsole.Input;

namespace PrimitiveAdventure.Screens.Items;

public class ChooseItemScreen: GlobalScreen
{
    private readonly Player _player;
    private readonly IItem _item;
    
    public ChooseItemScreen(Player player, IItem item)
    {
        _player = player;
        _item = item;
        
        var stayButton = new KeyedButton("Оставить".Prepare(), Keys.Escape)
        {
            Position = (0, Height - 1)
        };
        stayButton.Click += StayButtonOnClick;
        Controls.Add(stayButton);
        
        var changeButton = new KeyedButton("Взять".Prepare(), Keys.Space)
        {
            Position = (Width / 2, Height - 1)
        };
        changeButton.Click += ChangeButtonOnClick;
        Controls.Add(changeButton);
        
        Children.Add(new ItemView(Width / 2, Height - 1, player.Equipment[item.Kind]));
        
        Children.Add(new ItemView(Width / 2, Height - 1, item)
        {
            Position = (Width / 2, 0)
        });
    }

    private void StayButtonOnClick(object? sender, EventArgs e)
    {
    }

    private void ChangeButtonOnClick(object? sender, EventArgs e)
    {
        _player.Equipment.setItem(_item);
    }
}