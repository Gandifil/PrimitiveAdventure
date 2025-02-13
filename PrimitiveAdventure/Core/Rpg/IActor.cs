﻿using PrimitiveAdventure.Core.Rpg.Abilities;
using PrimitiveAdventure.Core.Rpg.Controlling;
using PrimitiveAdventure.Core.Rpg.Effects;
using PrimitiveAdventure.Core.Rpg.Fight;
using PrimitiveAdventure.Core.Rpg.Utils;

namespace PrimitiveAdventure.Core.Rpg;

public interface IActor
{
    string Name { get; }
    
    Point LocalPosition { get; }
    
    ILimitedValue<int> Health { get; }
    
    ILimitedValue<int> Magic { get; }
    
    ILimitedValue<int> Stamina { get; }
    
    IReadOnlyList<IAbility> Abilities { get; }
    
    ITeam Team { get; }
    
    IParameters Parameters { get; }
    
    IEffectHost Effects { get; }
    
    IControllable Controller { get; }
    
    int Damage { get; }
    
    bool IsAlive { get; }

    bool IsDefenced { get; }

    IActor[] EnemiesOnAttackLine();

    event Action Attacked;
}