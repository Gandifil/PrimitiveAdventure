using PrimitiveAdventure.Core;
using PrimitiveAdventure.Core.Global;
using PrimitiveAdventure.SadConsole.Screens;
using PrimitiveAdventure.Ui;
using PrimitiveAdventure.Utils;

namespace PrimitiveAdventure.Screens;

public class MapScreen: BaseScreen
{
    public const int CELL_WIDTH = 15;
    public const int CELL_HEIGHT = 7;
    
    private readonly IGlobalMap _globalMap;
    private readonly PlayerCell _player;
    
    public MapScreen(int width, int height, IGlobalMap globalMap, PlayerCell player) : base(width, height)
    {
        _globalMap = globalMap;
        _player = player;

        Update();
    }
    
    public void Update()
    {
        Children.Clear();
        this.Fill(new ColoredGlyph());

        var playerCellPosition = _player.Position * (CELL_WIDTH, CELL_HEIGHT) + (CELL_WIDTH / 2, CELL_HEIGHT / 2);
        var offset = playerCellPosition - (Width / 2, Height / 2) + (0, 1);
        offset *= -1;
        
        var view = new Rectangle(0, 0, Width, Height);
        for (int x = 0; x < _globalMap.Size.X; x++)
        for (int y = 0; y < _globalMap.Size.Y; y++)
        {
            var rect = new Rectangle(CELL_WIDTH * x + offset.X, CELL_HEIGHT * y + offset.Y, CELL_WIDTH + 1, CELL_HEIGHT + 1);
            if (!view.Contains(rect))
                continue;
            Surface.DrawBox(rect, ShapeParameters.CreateStyledBoxThin(Color.Aqua));

            var cell = _globalMap[x, y];
            if (cell is not null)
                DrawCell(cell, rect);
        }
        Surface.ConnectLines(ICellSurface.ConnectedLineThin);
        
        DrawDoors(offset);
    }

    private void DrawDoors(Point offset)
    {
        var view = new Rectangle(0, 0, Width, Height);
        
        for (int x = 0; x < _globalMap.Size.X; x++)
        for (int y = 0; y < _globalMap.Size.Y; y++)
        {
            var rect = new Rectangle(CELL_WIDTH * x + offset.X, CELL_HEIGHT * y + offset.Y, CELL_WIDTH + 1, CELL_HEIGHT + 1);
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

    private void DrawCell(IGlobalMapCell cell, Rectangle rect)
    {
        var screenObject = cell.View;
        screenObject.Position = new Point(rect.X + 1, rect.Y + 1);
        Children.Add(screenObject);
    }
}