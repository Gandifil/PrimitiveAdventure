﻿using PrimitiveAdventure.Core;
using SadConsole.Components;

namespace PrimitiveAdventure.Screens;

public class GlobalModeScreen: Console 
{
    private readonly GameState _gameState;
    
    public GlobalModeScreen(GameState gameState) : base(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT)
    {
        _gameState = gameState;
        
        Cursor.PrintAppearanceMatchesHost = false;
        
        RenderMap(_gameState.GlobalMap);
    }

    private void RenderMap(IGlobalMap map)
    {
        const int CELL_WIDTH = 15;
        const int CELL_HEIGHT = 7;
        
        for (int x = 0; x < map.Size.X; x++)
        for (int y = 0; y < map.Size.Y; y++)
        {
            var rect = new Rectangle(CELL_WIDTH * x, CELL_HEIGHT * y, CELL_WIDTH + 1, CELL_HEIGHT + 1);
            Surface.DrawBox(rect, 
                ShapeParameters.CreateStyledBoxThin(Color.Aqua));

            var cell = map[x, y];
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