﻿namespace PrimitiveAdventure.Core.Global;

public interface IGlobalMapCell
{
    string Name { get; }
    
    string? Resource { get; }

    void OnCollisionWith(Player player)
    {
    }
}