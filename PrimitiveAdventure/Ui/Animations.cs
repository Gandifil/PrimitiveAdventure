namespace PrimitiveAdventure.Ui;

public class Animations
{
    public static AnimatedScreenObject Noise(int width, int height, Color color)
    {
        var animation = new AnimatedScreenObject(
            name: "Noise", 
            width: width,
            height: height)
        {
            AnimationDuration = TimeSpan.FromMilliseconds(100) * 4,
        };
        animation.AnimationStateChanged += (sender, args) =>
        {
            if (args.NewState == AnimatedScreenObject.AnimationState.Finished)
                animation.Restart();
        };
        for (int i=0; i < 4; i++)
        {
            var frame = animation.CreateFrame();
            frame.DefaultBackground = Color.Transparent;
            frame.DefaultForeground = color;
            for (int x = 0; x < frame.Width; x++)
            for (int y = 0; y < frame.Height; y++)
                frame[x, y].Glyph = Random.Shared.Next() % 2 == 0 ? 176 : 0;
        }
        return animation;
    }
}