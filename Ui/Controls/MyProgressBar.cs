using SadConsole.Effects;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Ui.Controls;

public class MyProgressBar: SadConsole.UI.Controls.ProgressBar
{
    private readonly Blinker _effect;

    public MyProgressBar(int width, int height, HorizontalAlignment horizontalAlignment) : base(width, height, horizontalAlignment)
    {
        _effect = new Blinker()
        {
            Duration = System.TimeSpan.MaxValue,
            BlinkOutForegroundColor = Color.Black,
            BlinkSpeed = TimeSpan.FromMilliseconds(200),
            RunEffectOnApply = true
        };
    }

    public override void UpdateAndRedraw(TimeSpan time)
    {
        Surface.Effects.UpdateEffects(time);
        IsDirty = true;
        base.UpdateAndRedraw(time);
        Surface.SetEffect(Surface.GetCells(Surface.Area), _effect);
    }
}