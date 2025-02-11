using PrimitiveAdventure.Core.Rpg.Controlling;

namespace PrimitiveAdventure.Core.Rpg.Actors;

public class Dog: Actor
{
    public Dog(int power = 1)
    {
        Name = "ацкий пес";
        Controller = new AiController(this);
        Parameters[Parameters.Kind.Armor].Value += power / 2;
        Health.MaxValue.Value += power*5;
        Health.Value += power*5;
    }
}