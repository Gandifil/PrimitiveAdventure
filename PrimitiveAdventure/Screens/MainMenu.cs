using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.SadConsole.Controls;
using PrimitiveAdventure.Screens.Base;
using PrimitiveAdventure.Screens.Saves;
using PrimitiveAdventure.Screens.Views;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Screens;

public class MainMenu: GlobalScreen
{
    public MainMenu()
    {
        var start = new Button("Начать".Prepare());
        start.Click += (_, _) =>
        {
            new SaveChooseScreen()
            {
                BackScreen = this,
            }.Start();
        };
        
        var credit = new Button("Об игре".Prepare());
        credit.Click += (_, _) =>
        {
            new BackScreen<Credits>((_, _) => this.Start()).Start();
        };
        
        var exit = new Button("Выход".Prepare());
        exit.Click += (_, _) => Environment.Exit(0);

        Controls.GetControlCursor()
            .ToStart()
            .NewLine()
            .SetShift(1)
            .Print(start)
            .Print(credit)
            .Print(exit);
    }
}