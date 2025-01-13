namespace PrimitiveAdventure.Core.Rpg;

public class FightMapView
{
    
    public IReadOnlyCollection<Actor> Friends { get; }
    
    public IReadOnlyCollection<Actor> Enemies { get; private set; }
    
    public IReadOnlyCollection<Actor> All { get; set; }

    private readonly FightProcess _fightProcess;

    public bool IsInverted { get; }

    public FightMapView(FightProcess fightProcess, bool isInverted)
    {
        _fightProcess = fightProcess;
        IsInverted = isInverted;

        if (IsInverted)
        {
            Enemies = fightProcess.Team1;
            Friends = fightProcess.Team2;
        }
        else
        {
            Enemies = fightProcess.Team2;
            Friends = fightProcess.Team1;
        }
    }
}