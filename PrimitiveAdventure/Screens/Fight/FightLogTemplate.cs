using PrimitiveAdventure.SadConsole;

namespace PrimitiveAdventure.Screens.Fight;

public record FightLogTemplate(bool? IsCritical, bool? IsBlocked, string Template)
{
    public string Build(string Name, string targetName = null, int damage = 0, int positive = 0) => string.Format(
        Template,
        ColoredStringBuilder.Name(Name), //0
        ColoredStringBuilder.Name(targetName), //1
        ColoredStringBuilder.Damage("-" + damage), //2
        "рукой", //3,
        ColoredStringBuilder.Positive(damage.ToString()) //4
    );
}