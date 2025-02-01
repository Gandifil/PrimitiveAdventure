using PrimitiveAdventure.Core.Rpg.Controlling;

namespace PrimitiveAdventure.Core.Rpg.Actors;

public class Dog: Actor
{
    public Dog()
    {
        Name = "ацкий пес";
        Parameters[Parameters.Kind.Armor].Value += 5;
    }
}