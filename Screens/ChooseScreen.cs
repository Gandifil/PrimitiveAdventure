using PrimitiveAdventure.Ui;
using PrimitiveAdventure.Ui.Controls;
using SadConsole.Input;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Screens;

public class ChooseScreen<T>: BaseScreen where T: class, INamed
{
    private readonly IReadOnlyCollection<T> _elements;
    //private readonly TextBox _description;
    
    public T? Selected { get; private set; }
    
    public required BaseScreen NextScreen { get; init; }

    public event Action<T> SelectedSuccessfully; 

    private static List<Keys> KeysList = new List<Keys>()
    {
        Keys.D1,
        Keys.D2,
        Keys.D3,
        Keys.D4,
        Keys.D5,
        Keys.D6,
        Keys.D7,
        Keys.D8,
        Keys.D9,
    };
    
    public ChooseScreen(int width, int height, IReadOnlyList<T> elements) : base(width, height)
    {
        _elements = elements;

        var exitButton = new KeyedButton("назад".Prepare(), Keys.Escape)
        {
            Position = (1, height - 1)
        };
        exitButton.Click += ExitButtonOnClick;
        Controls.Add(exitButton);

        var enterButton = new KeyedButton("выбрать".Prepare(), Keys.Enter)
        {
            Position = (width-"выбрать".Length -15, height - 1)
        };
        enterButton.Click += EnterButtonOnClick;
        Controls.Add(enterButton);

        var list = new ListBox(width / 2, height - 3)
        {
            Position = (1, 1)
        };
        list.SelectedItemChanged += ListOnSelectedItemChanged;
        Controls.Add(list);
        for (int i = 0; i < elements.Count; i++)
        {
            var element = elements[i];
            // var key = KeysList[i];
            // var button = new KeyedButton(element.Name.Prepare(), key);
            //button.Position
            list.Items.Add(element);
        }
    }

    private void EnterButtonOnClick(object? sender, EventArgs e)
    {
        if (Selected is null)
            return;
        
        SelectedSuccessfully?.Invoke(Selected);
        NextScreen.Start();
    }

    private void ExitButtonOnClick(object? sender, EventArgs e)
    {
        Selected = null;
        NextScreen.Start();
    }

    private void ListOnSelectedItemChanged(object? sender, ListBox.SelectedItemEventArgs e)
    {
        Selected = (T)e.Item!;

        UpdateDescription();
    }

    protected virtual void UpdateDescription()
    {
    }
}