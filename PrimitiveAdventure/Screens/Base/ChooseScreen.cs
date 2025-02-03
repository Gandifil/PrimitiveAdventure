using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.SadConsole.Controls;
using PrimitiveAdventure.SadConsole.Screens;
using SadConsole.Input;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Screens.Base;

public class ChooseScreen<T>: BaseScreen where T: class
{
    private readonly Button enterButton;
    private readonly Console _titleConsole;
    private readonly IReadOnlyCollection<T> _elements;
    protected IEntityView<T> _entityView;
    
    public T? Selected { get; private set; }
    
    public BaseScreen? BackScreen { get; init; }
    
    private string? _title;

    public string? Title
    {
        get => _title; 
        init
        {
            _title = value;
            _titleConsole.Surface.Clear();
            _titleConsole.Cursor.Position = new Point(0, 0);
            if (_title is not null)
                _titleConsole.Cursor.Print(_title.Prepare());
        }
    }

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
    
    public ChooseScreen(int width, int height, IReadOnlyList<T> elements, bool showExitButton = true) : base(width, height)
    {
        _elements = elements;
        
        Children.Add(_titleConsole = new Console(width, 1));

        if (showExitButton)
        {
            var exitButton = new KeyedButton("назад".Prepare(), Keys.Escape)
            {
                Position = (1, height - 1)
            };
            exitButton.Click += ExitButtonOnClick;
            Controls.Add(exitButton);
        }

        enterButton = new KeyedButton("выбрать".Prepare(), Keys.Enter)
        {
            Position = (width-"выбрать".Length -15, height - 1),
            IsEnabled = false
        };
        enterButton.Click += EnterButtonOnClick;
        Controls.Add(enterButton);

        var list = new ListBox(width / 2, height - 4)
        {
            Position = (1, 3)
        };
        list.SelectedItemChanged += ListOnSelectedItemChanged;
        Controls.Add(list);
        for (int i = 0; i < elements.Count; i++)
        {
            var element = elements[i];
            list.Items.Add(element);
        }
    }

    private void EnterButtonOnClick(object? sender, EventArgs e)
    {
        if (Selected is null)
            return;
        
        SelectedSuccessfully?.Invoke(Selected);
    }

    private void ExitButtonOnClick(object? sender, EventArgs e)
    {
        Selected = null;
        BackScreen.Start();
    }

    private void ListOnSelectedItemChanged(object? sender, ListBox.SelectedItemEventArgs e)
    {
        Selected = (T)e.Item!;
        _entityView.Set(Selected);
        enterButton.IsEnabled = GetEnterIsEnabled();
    }
    
    protected virtual bool GetEnterIsEnabled() => true;

    protected void SetView<TView>(TView view) where TView : IScreenObject, IEntityView<T>
    {
        view.Position = (Width / 2, 2);
        _entityView = view;
        Children.Add(view);
    }
}