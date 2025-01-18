using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Ui.Controls;
using SadConsole.UI;

namespace PrimitiveAdventure.Screens.Views;

public class ActorResourcesView: ControlsConsole
{
    private readonly IActor _actor;
    
    public IActor Actor => _actor;
    
    public ActorResourcesView(int width, IActor actor) : base(width, 2)
    {
        _actor = actor;
        
        var magicBar = new MyProgressBar(width, 1, HorizontalAlignment.Left)
        {
            Position = (0, 0),
            Progress = actor.Magic.Progress,
            BarColor = Color.Blue,
        };
        actor.Magic.Changed += () => magicBar.Progress = actor.Magic.Progress;
        Controls.Add(magicBar);

        var staminaBar = new MyProgressBar(width, 1, HorizontalAlignment.Left)
        {
            Position = (0, 1),
            Progress = actor.Stamina.Progress,
            BarColor = Color.YellowGreen,
        };
        actor.Stamina.Changed += () => staminaBar.Progress = actor.Stamina.Progress;
        Controls.Add(staminaBar);
    }
    
    
}