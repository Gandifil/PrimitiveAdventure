using PrimitiveAdventure.Utils;

namespace PrimitiveAdventure.Screens.Views;

public class ResourceView: ScreenSurface
{
    private readonly IEnumerable<string> _lines;

    public Color Foreground
    {
        get => Surface.DefaultForeground;
        set { Surface.DefaultForeground = value; IsDirty = true; }
    }

    public ResourceView(int width, int height, string resource) : base(width, height)
    {
        _lines = Services.Resources.Load<IEnumerable<string>>(resource);
    }

    public override void Render(TimeSpan delta)
    {
        if (IsDirty)
        {
            Surface.Clear();
            int i = 0;
            foreach (var line in _lines)
                Surface.Print(0, i++, line);
            IsDirty = false;
        }
        base.Render(delta);
    }
}