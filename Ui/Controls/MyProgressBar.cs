using SadConsole.Effects;

namespace PrimitiveAdventure.Ui.Controls;

public class MyProgressBar: SadConsole.UI.Controls.ProgressBar
{
    public MyProgressBar(int width, int height, HorizontalAlignment horizontalAlignment) : base(width, height, horizontalAlignment)
    {
    }

    public override void UpdateAndRedraw(TimeSpan time)
    {
        base.UpdateAndRedraw(time);
        Surface.SetEffect(Surface.GetCells(Surface.Area), new Blinker()
        {
            // Blink forever
            Duration = System.TimeSpan.MaxValue,
            BlinkOutForegroundColor = Color.Black,
            // Every half a second
            BlinkSpeed = TimeSpan.FromMilliseconds(500),
            RunEffectOnApply = true
        });
    }
}