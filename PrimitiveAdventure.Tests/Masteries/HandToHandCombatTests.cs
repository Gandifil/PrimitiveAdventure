using PrimitiveAdventure.Core;
using PrimitiveAdventure.Core.Rpg.Abilities;
using PrimitiveAdventure.Core.Rpg.Actors;
using PrimitiveAdventure.Core.Rpg.Fight;
using PrimitiveAdventure.Masteries.HandToHandCombat;

namespace PrimitiveAdventure.Tests.Masteries;

public class HandToHandCombatTests
{
    private readonly Ability _voidLoop = new VoidLoop();
    
    public void Combo_UseDifferentAbility_WithBonus()
    {
        // arrange
        var player = new Player();
        _voidLoop.SetOwner(player);
        var actor = new Dog();
        
        // act
        _voidLoop.Use(actor);
        _voidLoop.Use(actor);
        
        // assert
        Assert.True(player.Effects.Has<ComboEffect>());
    }
    
    [Fact]
    public void Combo_UseSameAbility_NoBonus()
    {
        // arrange
        var player = new Player();
        _voidLoop.SetOwner(player);
        var actor = new Dog();
        
        var fight = new FightBuilder();
        fight.AddPlayer(player);
        fight.Add(actor);
        fight.Build().Start();
        
        // act
        _voidLoop.Use(actor);
        _voidLoop.Use(actor);
        
        // assert
        Assert.False(player.Effects.Has<ComboEffect>());
    }
}