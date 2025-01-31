using System.Reflection;
using PrimitiveAdventure.Core;

namespace PrimitiveAdventure.Utils;

public static class Content
{
    public static readonly Saver<Player> Saves = new (
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().GetName().Name),
        "bin");
}