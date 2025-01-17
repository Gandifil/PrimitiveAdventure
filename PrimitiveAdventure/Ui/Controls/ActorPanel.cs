using PrimitiveAdventure.Core.Rpg;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Ui.Controls;

public class ActorPanel: Panel
{
    private readonly IActor _actor;
    
    public IActor Actor => _actor;
    
    public ActorPanel(int width, int height, IActor actor) : base(width, height)
    {
        _actor = actor;
         
        var pb = new MyProgressBar(width, 1, HorizontalAlignment.Left)
        {
            Position = (0, height - 1),
            Progress = actor.Health.Progress,
        };
        Add(pb);

        actor.Health.Changed += () => pb.Progress = actor.Health.Progress;
    }
    
}