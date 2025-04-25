using PrimitiveAdventure.Core;
using PrimitiveAdventure.Core.Global;
using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Actors;
using PrimitiveAdventure.Core.Rpg.Fight;
using PrimitiveAdventure.Core.Rpg.Masteries;
using PrimitiveAdventure.Masteries.HandToHandCombat;
using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.Screens.Fight;

namespace PrimitiveAdventure.Utils;

public class CmdParser
{
    private readonly string[] _args;

    public CmdParser(string[] args)
    {
        _args = args;
    }
    
    public void Run()
    {
        var player = new Player();
        if (Has("test"))
        {
            var level = GetInt("level");
            if(level.HasValue)
                player.LevelUp(level.Value);
        
            new GameState(GlobalMap.TestMap(), player).StartScreen();
        }
        else if (Has("test-fight"))
        {
            player.Masteries.Add(new HandToHandCombatMastery()).Get<AbilityTalent<VoidLoop>>()!.Upgrade(player);
            new GameState(GlobalMap.TestMap(), player);
            var builder = new FightBuilder();
            builder.AddPlayer(player);
            var searcher = TypeSearcher.ForExecutingAssembly();
            foreach (var enemyName in GetArray("enemy"))
                builder.Add(searcher.SearchAndActivate<Actor>(enemyName, 1));
            var process = builder.Build().Start();
            new FightScreen(player, process).Start();
        }
    }

    private bool Has(string value) => _args.Contains("--" + value);
    
    private string? Get(string name) {
        var arg = _args.FirstOrDefault(x => x.StartsWith($"--{name}="));
        var valueStr = arg?.Split('=')[1];
        return valueStr;
    }

    private string[] GetArray(string name) => 
        _args.Where(x => x.StartsWith($"--{name}=")).Select(x => x.Split('=')[1]).ToArray();
    
    private int? GetInt(string name) {
        var valueStr = Get(name);
        return valueStr is null ? null : int.Parse(valueStr);
    }
}