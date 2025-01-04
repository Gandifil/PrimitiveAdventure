using PrimitiveAdventure.Core;

namespace PrimitiveAdventure.Screens;

public class MapScreen: Console
{
    private readonly IGlobalMap _globalMap;
    
    public MapScreen(int width, int height, IGlobalMap globalMap) : base(width, height)
    {
        _globalMap = globalMap;
        
        Cursor.PrintAppearanceMatchesHost = false;

        Update();
    }
    
    public void Update()
    {
        const int CELL_WIDTH = 15;
        const int CELL_HEIGHT = 7;

        this.Fill(new ColoredGlyph());
        
        for (int x = 0; x < _globalMap.Size.X; x++)
        for (int y = 0; y < _globalMap.Size.Y; y++)
        {
            var rect = new Rectangle(CELL_WIDTH * x, CELL_HEIGHT * y, CELL_WIDTH + 1, CELL_HEIGHT + 1);
            Surface.DrawBox(rect, 
                ShapeParameters.CreateStyledBoxThin(Color.Aqua));

            var cell = _globalMap[x, y];
            if (cell is not null)
            {
                var resource = cell.Resource;
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
    }
}