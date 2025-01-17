using System.Collections;

namespace PrimitiveAdventure.Core.Global;

public interface IGlobalMap
{
    IGlobalMapCell? this[int row, int column] { get; }
    
    Point Size { get; }

    bool IsDoorOpened(int row, int column, bool isRight);
}

public class GlobalMap: IGlobalMap
{
    private readonly IGlobalMapCell[,] _cells;
    
    private readonly BitArray _doors;
    
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
        _doors = new BitArray(Size.X * Size.Y * 2);
    }

    public bool IsDoorOpened(int row, int column, bool isRight)
        => _doors[GetDoorsIndex(row, column, isRight)];

    private int GetDoorsIndex(int row, int column, bool isRight)
    {
        return 2 * (Size.X * row + column) + (isRight ? 1 : 0);
    }

    public void Spawn(IGlobalMapCell cell, int row, int column)
    {
        _cells[row, column] = cell;
    }

    public void Move(Point from, Point to)
    {
        this[to] = this[from];
        this[from] = null;
        
        var doorCell = from.X + from.Y > to.X + to.Y ? to : from;
        var isRightDoor = from.X != to.X;
        _doors[GetDoorsIndex(doorCell.X, doorCell.Y, isRightDoor)] = true;
    }
}