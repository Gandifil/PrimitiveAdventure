using System.Runtime.Serialization;
using PrimitiveAdventure.Core.Rpg.Utils;
using SadConsole.Effects;
using SadConsole.UI;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Ui.Controls;

[DataContract]
public class ProgressBar: ControlBase
{
    private readonly ILimitedValue<int> _value;
        
    public ProgressBar(int width, int height, ILimitedValue<int> value) : base(width, height)
    {
        _value = value;
        CanFocus = false;
    }

    public override void UpdateAndRedraw(TimeSpan time)
    {
        if (!IsDirty) return;

        Colors colors = FindThemeColors();

        RefreshThemeStateColors(colors);

        ColoredGlyphBase appearance = ThemeState.GetStateAppearance(State);
        Surface.Fill(
            appearance.Foreground,
            appearance.Background,
            appearance.Glyph, null);

        var highlightedWidth = Width * _value.Value.Value / _value.MaxValue.Value;
        
        Surface.Fill(0, 0, highlightedWidth, 
            appearance.Foreground, appearance.Background, 176);
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