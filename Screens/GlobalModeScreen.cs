using System.IO.MemoryMappedFiles;
using PrimitiveAdventure.Core;
using SadConsole.Components;
using SadConsole.Input;

namespace PrimitiveAdventure.Screens;

public class GlobalModeScreen: Console 
{
    private readonly GameState _gameState;
    private readonly MapScreen _mapScreen;

    private const int SEPARATE_SCREEN_WIDTH = 30;
    
    public GlobalModeScreen(GameState gameState) : base(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT)
    {
        _gameState = gameState;
        
        Children.Add(_mapScreen = new MapScreen(
            GameSettings.GAME_WIDTH - SEPARATE_SCREEN_WIDTH, 
            GameSettings.GAME_HEIGHT, _gameState.GlobalMap));
    }
    
    public override bool ProcessKeyboard(Keyboard keyboard)
    {
        Point? shift = null;
        
        if (keyboard.IsKeyReleased(Keys.W) || keyboard.IsKeyReleased(Keys.Up))
            shift =  new Point(0, -1);
        
        if (keyboard.IsKeyReleased(Keys.S) || keyboard.IsKeyReleased(Keys.Down))
            shift =  new Point(0, 1);
        
        if (keyboard.IsKeyReleased(Keys.A) || keyboard.IsKeyReleased(Keys.Left))
            shift =  new Point(-1, 0);
        
        if (keyboard.IsKeyReleased(Keys.D) || keyboard.IsKeyReleased(Keys.Right))
            shift =  new Point(1, 0);

        if (shift.HasValue)
        {
            _gameState.MovePlayer(shift.Value);
            _mapScreen.Update();
            return true;
        }
        return base.ProcessKeyboard(keyboard);
    }
}