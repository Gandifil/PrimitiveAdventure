using SadConsole.Readers;

namespace PrimitiveAdventure.Screens.Views;

public class PlaysciiView: ScreenSurface
{
    public PlaysciiView(int width, int height, string name) : base(width, height)
    {
        ScreenSurface image = Playscii.ToScreenSurface($"Resources/{name}.psci", 
            Game.Instance.DefaultFont,
            paletteFileName: "Resources/Palettes/c64_original.png");
        image.Surface.DefaultBackground = Color.DarkGray.GetDarkest();
        image.Surface.Clear();
        Children.Add(image);
        
        image.UsePixelPositioning = true;
        image.Position = (WidthPixels / 2 - image.AbsoluteArea.Width / 2, 
            HeightPixels / 2 - image.AbsoluteArea.Height / 2);
    }
}