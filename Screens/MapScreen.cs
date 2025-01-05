using PrimitiveAdventure.Core;
using PrimitiveAdventure.Core.Global;

namespace PrimitiveAdventure.Screens;

public class MapScreen: Console
{
    private readonly IGlobalMap _globalMap;
    private readonly IPlayer _player;
    //private readonly Lazy<AnimatedScreenObject> _enemyGroupAnimation = new(Create);
    
    public MapScreen(int width, int height, IGlobalMap globalMap, IPlayer player) : base(width, height)
    {
        _globalMap = globalMap;
        _player = player;

        Cursor.PrintAppearanceMatchesHost = false;

        Update();
    }
    
    public void Update()
    {
        const int CELL_WIDTH = 15;
        const int CELL_HEIGHT = 7;

        Children.Clear();
        this.Fill(new ColoredGlyph());

        var playerCellPosition = _player.GlobalPosition * (CELL_WIDTH, CELL_HEIGHT) + (CELL_WIDTH / 2, CELL_HEIGHT / 2);
        var offset = playerCellPosition - (Width / 2, Height / 2) + (0, 1);
        offset *= -1;

        var view = new Rectangle(0, 1, Width, Height);
        
        for (int x = 0; x < _globalMap.Size.X; x++)
        for (int y = 0; y < _globalMap.Size.Y; y++)
        {
            var rect = new Rectangle(CELL_WIDTH * x, CELL_HEIGHT * y, CELL_WIDTH + 1, CELL_HEIGHT + 1);
            rect = rect.ChangePosition(offset);
            if (!view.Contains(rect))
                continue;
            Surface.DrawBox(rect, 
                ShapeParameters.CreateStyledBoxThin(Color.Aqua));
            
            if (_globalMap.IsDoorOpened(x, y, false))
                this.SetGlyph(rect.X + CELL_WIDTH/2, rect.MaxExtentY, 187);
            
            if (_globalMap.IsDoorOpened(x, y, true))
                this.SetGlyph(rect.MaxExtentX, rect.Y + CELL_HEIGHT/2, 187);

            var cell = _globalMap[x, y];
            if (cell is not null)
            {
                var resource = cell.Resource;
                if (cell is EnemyGroup)
                    CreateAnimation().Position = new Point(rect.X + 1, rect.Y + 1);
                if (string.IsNullOrEmpty(resource))
                    Cursor
                        .SetPrintAppearance(Color.Yellow)
                        .Move(rect.X + 1, rect.Y + 1)
                        .Print(cell.Name);
                else
                {
                    var lines = File.ReadLines("Resources/" + resource + ".txt");

                    Cursor.Move(rect.X + 1, rect.Y + 1)
                        .SetPrintAppearance(Color.Green);
                    foreach (var line in lines)
                    {
                        Cursor.Print(line);
                        Cursor.Row++;
                        Cursor.Column = rect.X + 1;
                    }
                }
            }
        }
        Surface.ConnectLines(ICellSurface.ConnectedLineThin);
        
        for (int x = 0; x < _globalMap.Size.X; x++)
        for (int y = 0; y < _globalMap.Size.Y; y++)
        {
            var rect = new Rectangle(CELL_WIDTH * x, CELL_HEIGHT * y, CELL_WIDTH + 1, CELL_HEIGHT + 1);
            rect = rect.ChangePosition(offset);
            if (!view.Contains(rect))
                continue;

            if (_globalMap.IsDoorOpened(x, y, false))
            {
                this.SetGlyph(rect.X + CELL_WIDTH/2, rect.MaxExtentY, 217);
                this.SetGlyph(rect.X + CELL_WIDTH/2 + 1, rect.MaxExtentY, 0);
            }

            if (_globalMap.IsDoorOpened(x, y, true))
            {
                this.SetGlyph(rect.MaxExtentX, rect.Y + CELL_HEIGHT/2, 217);
                this.SetGlyph(rect.MaxExtentX, rect.Y + CELL_HEIGHT/2 + 1, 0);
            }
        }
    }

    private AnimatedScreenObject CreateAnimation()
    {
        var animation = new AnimatedScreenObject(
            name: "Pulse", 
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
        
        Children.Add(animation);
        animation.Start();
        return animation;
    }
}