using System.Runtime.Serialization;
using SadConsole.Input;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.SadConsole.Controls;

public class KeyedButton: Button
{
    [DataMember]
    public Keys Key { get; }
    
    public KeyedButton(string text, Keys key) : base($"{text}[{ConvertKey(key)}]")
    {
        Key = key;
    }

    private static string ConvertKey(Keys key)
    {
        return key switch
        {
            Keys.Right => char.ConvertFromUtf32(26),//"[c:sg 26] ",
            _ => key.ToString()
        };
    }

    public bool ForceProcessKeyboard(Keyboard info)
    {
        if (IsEnabled && info.IsKeyReleased(Key))
        {
            OnClick();
            return true;
        }

        return false;
    }
}