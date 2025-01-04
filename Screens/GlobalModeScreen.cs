using PrimitiveAdventure.Core;

namespace PrimitiveAdventure.Screens;

public class GlobalModeScreen: ScreenSurface
{
    private readonly GameState _gameState;
    
    public GlobalModeScreen(GameState gameState) : base(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT)
    {
        _gameState = gameState;
        
        RenderMap(_gameState.GlobalMap);
    }

    private void RenderMap(IGlobalMap map)
    {
        const int CELL_WIDTH = 15;
        const int CELL_HEIGHT = 5;
        
        for (int x = 0; x < map.Size.X; x++)
        for (int y = 0; y < map.Size.Y; y++)
        {
            Surface.DrawBox(new Rectangle(CELL_WIDTH * x, CELL_HEIGHT * y, CELL_WIDTH + 1, CELL_HEIGHT + 1), 
                ShapeParameters.CreateStyledBoxThin(Color.Aqua));
            
        }
        Surface.ConnectLines(ICellSurface.ConnectedLineThin);
    }
}