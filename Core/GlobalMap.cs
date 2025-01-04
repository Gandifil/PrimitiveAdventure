namespace PrimitiveAdventure.Core;

public interface IGlobalMap
{
    IGlobalMapCell? this[int row, int column] { get; }
    
    Point Size { get; }
}

public class GlobalMap: IGlobalMap
{
    private readonly IGlobalMapCell[,] _cells;
    
    public IGlobalMapCell? this[int row, int column] => _cells[row, column];

    public IGlobalMapCell? this[Point index]
    {
        get => _cells[index.X, index.Y];
        set => _cells[index.X, index.Y] = value!;
    }

    public Point Size { get; }
    
    public GlobalMap(Point size)
    {
        Size = size;
        _cells = new IGlobalMapCell[Size.X, Size.Y];
    }

    public void Spawn(IGlobalMapCell cell, int row, int column)
    {
        _cells[row, column] = cell;
    }

    public void Move(Point from, Point to)
    {
        this[to] = this[from];
        this[from] = null;
    }
}