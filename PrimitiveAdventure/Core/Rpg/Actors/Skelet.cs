using PrimitiveAdventure.Core.Rpg.Controlling;

namespace PrimitiveAdventure.Core.Rpg.Actors;

public class Skelet: Actor
{
    public Skelet(int power = 1)
    {
        Name = "Скелет";
        Controller = new AiDefenceController(this);
        Parameters[Parameters.Kind.Armor].Value += power;
        Health.MaxValue.Value += power*5;
        Health.Value += power*5;
    }
}