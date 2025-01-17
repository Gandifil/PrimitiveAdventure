namespace PrimitiveAdventure.Screens;

public class Animations
{
    public static AnimatedScreenObject ForEnemyGroup()
    {
        var animation = new AnimatedScreenObject(
            name: "EnemyGroup", 
            width: 4,
            height: 4)
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
            frame.DefaultForeground = Color.Red;
            for (int x = 0; x < frame.Width; x++)
            for (int y = 0; y < frame.Height; y++)
                frame[x, y].Glyph = Random.Shared.Next() % 2 == 0 ? 176 : 0;
        }
        return animation;
    }
}