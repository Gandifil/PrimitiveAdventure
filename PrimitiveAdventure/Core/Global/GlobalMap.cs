﻿using System.Collections;
using PrimitiveAdventure.Core.Rpg.Actors;
using PrimitiveAdventure.Core.Rpg.Items;

namespace PrimitiveAdventure.Core.Global;

public interface IGlobalMap
{
    IGlobalMapCell? this[int row, int column] { get; }
    
    Point Size { get; }

    bool IsDoorOpened(int row, int column, bool isRight);
}

public class GlobalMap: IGlobalMap
{
    private readonly IGlobalMapCell?[,] _cells;
    
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

    public void Spawn(IGlobalMapCell cell)
    {
        while (true)
        {
            var row = Random.Shared.Next(0, Size.X);
            var column = Random.Shared.Next(0, Size.Y);
            if (_cells[row, column] is null)
            {
                Spawn(cell, row, column);
                return;
            }
        }
    }

    public void Move(Point from, Point to)
    {
        this[to] = this[from];
        this[from] = null;
        
        var doorCell = from.X + from.Y > to.X + to.Y ? to : from;
        var isRightDoor = from.X != to.X;
        _doors[GetDoorsIndex(doorCell.X, doorCell.Y, isRightDoor)] = true;
    }

    public static GlobalMap TestMap()
    {
        var map = new GlobalMap(new Point(20, 20));
        // map.Spawn(new Chest(), 1, 1);
        // map.Spawn(new Chest(), 4, 4);
        map.Spawn(new ItemCell(new Sword()), 3, 4);
        
        var enemyGroup1 = new EnemyGroup();
        enemyGroup1.Enemies.Add(new Dog());
        map.Spawn(enemyGroup1, 2, 3);

        for (int i = 0; i < 10; i++)
        {
            var enemyGroup = new EnemyGroup();
            enemyGroup.Enemies.Add(new Dog());
            map.Spawn(enemyGroup);
        }
        
        return map;
    }

    public static GlobalMap GenerateMap(int level = 0)
    {
        if (level == 0)
            return TestMap();
        
        throw new NotImplementedException();
    }
}