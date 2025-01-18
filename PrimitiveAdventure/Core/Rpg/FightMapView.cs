using PrimitiveAdventure.Core.Rpg.Abilities;

namespace PrimitiveAdventure.Core.Rpg;

public class IFightMapView
{
    public IReadOnlyCollection<IActor> Friends { get; }
    
    public IReadOnlyCollection<IActor> Enemies { get; }
    
    public IReadOnlyCollection<IActor> All { get; }
    
    bool IsInverted { get; }

    public IReadOnlyCollection<IActor> AllWhere(TargetKind targetKind)
    {
        switch (targetKind)
        {
            case TargetKind.Enemy: return Enemies;
            case TargetKind.Friend: return Friends;
            case TargetKind.Enemy | TargetKind.Friend: return All;
            default: return Array.Empty<IActor>();
        }
    }
}

public class FightMapView: IFightMapView
{
    
    public IReadOnlyCollection<Actor> Friends { get; }
    
    public IReadOnlyCollection<Actor> Enemies { get; }
    
    public IReadOnlyCollection<Actor> All { get; }

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

        All = _fightProcess.All;
    }
}