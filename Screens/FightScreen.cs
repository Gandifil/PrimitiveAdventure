using PrimitiveAdventure.Core.Rpg;

namespace PrimitiveAdventure.Screens;

public class FightScreen: Console
{
    const int CELL_WIDTH = 15;
    const int CELL_HEIGHT = 7;
    
    private readonly FightProcess _fightProcess;
    
    public FightScreen(int width, int height, FightProcess fightProcess) : base(width, height)
    {
        _fightProcess = fightProcess;

        Update();
    }

    public void Update()
    {
        Children.Clear();
        this.Clear();

        DrawWalls();
        DrawPlayer();
    }

    private void DrawWalls()
    {
        for (int x = 0; x < FightProcess.MAP_WIDTH; x++)
        for (int y = 0; y < FightProcess.MAP_HEIGHT; y++)
        {
            var rect = new Rectangle(CELL_WIDTH * x, CELL_HEIGHT * y, CELL_WIDTH + 1, CELL_HEIGHT + 1);
            Surface.DrawBox(rect, ShapeParameters.CreateStyledBoxThin(Color.LightGray));
        }
        Surface.ConnectLines(ICellSurface.ConnectedLineThin);
    }

    private void DrawPlayer()
    {
        var (x, y) = _fightProcess.Player.LocalPosition;
        var rect = new Rectangle(CELL_WIDTH * x, CELL_HEIGHT * y, CELL_WIDTH + 1, CELL_HEIGHT + 1);
            
        var lines = File.ReadLines("Resources/" + _fightProcess.Player.Resource + ".txt");

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