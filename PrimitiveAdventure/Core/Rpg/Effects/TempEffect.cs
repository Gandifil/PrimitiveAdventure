namespace PrimitiveAdventure.Core.Rpg.Effects;

public abstract class TempEffect: Effect
{
    public override void Tick(Actor actor)
    {
        Power.Value--;
    }
}