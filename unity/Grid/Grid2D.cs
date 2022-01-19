using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D<T> : IGrid
{
    private Vector2 dimensions;
    private Vector2 cellScale;
    // TODO: maybe this could be Vector3 so we can offset the grid on a certain Z axis
    private Vector2 offsetFromZero; 
    private bool useZ = false;
    private T[] cells;

    public int Cols { get { return (int)dimensions.x; } private set {} }
    public int Rows { get { return (int)dimensions.y; } private set {} }


    public Grid2D(int cols, int rows, Vector2 cellScale, Vector2 offsetFromZero, bool zFlag = false)
    {
        dimensions = new Vector2((float)cols, (float)rows);
        this.offsetFromZero = offsetFromZero;
        this.cellScale = cellScale;
        useZ = zFlag;
        cells = new T[cols * rows];
    }

    public Grid2D(int cols, int rows, bool zFlag = false)
    {
        dimensions = new Vector2((float)cols, (float)rows);
        this.offsetFromZero = Vector3.zero;
        this.cellScale = new Vector3(1, 1, 1);
        useZ = zFlag;
        cells = new T[cols * rows];
    }

    
    // Access a sepcific index in a one dimensional array with (x, y) coords using this method.
    public int Cart2Index(int x, int y)
    {
        return (y * Cols) + x;
    }
    // Cart2Index overload
    public int Cart2Index(Vector3 pos)
    {
        return ((int)pos.y * Cols) + (int)pos.x;
    }

    // Calculate the cartesian coordinates from the index of a one dimensional array 
    public Vector2 Index2Cart(int index)
    {
        if(Cols == 0) return Vector3.zero;
        return new Vector2(Mathf.FloorToInt(index % Cols), Mathf.FloorToInt(index / Cols));
    }
    
    // convert a world position to a grid position
    public Vector3 World2Grid(Vector3 worldPos)
    {
        int x = (int)Mathf.Floor((worldPos.x / cellScale.x) - offsetFromZero.x);
        int y = (int)Mathf.Floor((worldPos.y / cellScale.y) - offsetFromZero.y);
        return new Vector3(x, y);
    }
    
    // convert grid coordinates to that location in world space
    public Vector3 Grid2World(Vector3 gridPos)
    {
        Vector3 naturalOffset = new Vector3(cellScale.x / 2, cellScale.y / 2, 0);
        float x = (gridPos.x * cellScale.x) + naturalOffset.x + offsetFromZero.x;
        float y = (gridPos.y * cellScale.y) + naturalOffset.y + offsetFromZero.y;

        if(useZ) return new Vector3(x, 0f, y);
        else return new Vector3(x, y, 0f);
    }

    // convert grid coordinates to that location in world space
    public Vector3 Grid2World(int x, int y)
    {
        return Grid2World(new Vector3(x, y, 0));
    }

    // convert the index of a 1D array of a grid, into a world position
    public Vector3 Index2World(int index)
    {
        return Grid2World(Index2Cart(index));
    }

    // convert the world position to the index of a 1D array for a grid
    public int World2Index(Vector3 pos)
    {
        return Cart2Index(World2Grid(pos));
    }

    public bool Contains(Vector2 gridpos)
    {
       return (gridpos.x >= 0 && gridpos.x < Cols) && (gridpos.y >= 0 && gridpos.y < Rows);
    }

    public bool Contains(int x, int y)
    {
       return (x >= 0 && x < Cols) && (y >= 0 && y < Rows);
    }

    public T GetCell(int x, int y)
    {
        return cells[Cart2Index(x, y)];
    }

    public T GetCell(Vector2 pos)
    {
        return cells[Cart2Index((int)pos.x, (int)pos.y)];
    }

    public void SetCell(int x, int y, T obj)
    {
        cells[Cart2Index(x, y)] = obj;
    }

    public void SetCell(Vector2 pos, T obj)
    {
        cells[Cart2Index((int)pos.x, (int)pos.y)] = obj;
    }
}
