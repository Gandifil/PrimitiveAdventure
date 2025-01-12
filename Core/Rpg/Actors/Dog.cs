using PrimitiveAdventure.Core.Rpg.Controlling;

namespace PrimitiveAdventure.Core.Rpg.Actors;

public class Dog: Actor
{
    public Dog()
    {
        Name = "ацкий пес";
        Controller = new AiController(this);
    }
}