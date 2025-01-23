using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D
{
    private Vector2 cellScale;
    private Vector2 originOffset; 
    private Vector3 cellOffset;
    private bool useZ = false;
    private int cols;
    private int rows;

    public int Cols { get { return cols; } private set {} }
    public int Rows { get { return rows; } private set {} }
    public int Size { get { return cols * rows; } private set {} }
    public Vector2 CellScale { get { return cellScale; } private set {} }


    // Constructor
    public Grid2D(int cols, int rows, Vector2 cellScale, Vector2 originOffset, bool zFlag = false)
    {
        this.rows = rows == 0 ? 1 : rows;
        this.cols = cols == 0 ? 1 : cols;
        this.originOffset = originOffset;
        this.cellScale = cellScale;
        this.cellOffset = new Vector3(cellScale.x / 2, cellScale.y / 2, 0);
        useZ = zFlag;
    }

    // Constructor
    public Grid2D(int cols, int rows, bool zFlag = false)
    {
        this.rows = rows == 0 ? 1 : rows;
        this.cols = cols == 0 ? 1 : cols;
        this.originOffset = Vector3.zero;
        this.cellScale = new Vector3(1, 1, 1);
        this.cellOffset = new Vector3(cellScale.x / 2, cellScale.y / 2, 0);
        useZ = zFlag;
    }

    // converts cartesian coords to the corresponding index
    public int Cart2Index(int x, int y)
    {
        return (y * cols) + x;
    }

    // converts cartesian coords to the corresponding index
    public int Cart2Index(Vector2 pos)
    {
        return ((int)pos.y * cols) + (int)pos.x;
    }

    // convert grid coordinates to that location in world space
    public Vector3 Cart2World(Vector2 gridPos)
    {
        float x = (gridPos.x * cellScale.x) + cellOffset.x + originOffset.x;
        float y = (gridPos.y * cellScale.y) + cellOffset.y + originOffset.y;
        if(useZ) return new Vector3(x, 0f, y);
        else return new Vector3(x, y, 0f);
    }

    // convert grid coordinates to that location in world space
    public Vector3 Cart2World(int x, int y)
    {
        return Cart2World(new Vector2(x, y));
    }

    // convert an index to its cartesian coords
    public Vector2 Index2Cart(int index)
    {
        return new Vector2(Mathf.FloorToInt(index % cols), Mathf.FloorToInt(index / cols));
    }

    // convert the index of a 1D array of a grid, into a world position
    public Vector3 Index2World(int index)
    {
        return Cart2World(Index2Cart(index));
    }

    // convert a world position to a grid position
    public Vector2 World2Cart(Vector3 worldPos)
    {
        int x = (int)Mathf.Floor((worldPos.x / cellScale.x) - originOffset.x);
        int y = (int)Mathf.Floor((worldPos.y / cellScale.y) - originOffset.y);
        return new Vector2(x, y);
    }

    // convert a world position to an index 
    public int World2Index(Vector3 pos)
    {
        return Cart2Index(World2Cart(pos));
    }

    // checks if the provided Vector2 is contained withing the grid
    public bool Contains(Vector2 gridpos)
    {
       return (gridpos.x >= 0 && gridpos.x < cols) && (gridpos.y >= 0 && gridpos.y < rows);
    }

    // checks if the provided x and y coords are contained withing the grid
    public bool Contains(int x, int y)
    {
       return (x >= 0 && x < cols) && (y >= 0 && y < rows);
    }

    // checks if the provided Vector3 is contained withing the grid
    public bool Contains(Vector3 pos)
    {
       Vector2 gridpos = World2Cart(pos);
       return (gridpos.x >= 0 && gridpos.x < cols) && (gridpos.y >= 0 && gridpos.y < rows);
    }

    public bool Contains(int index)
    {
       return (index >= 0) && (index < Size);
    }

    // given a world position that is contained within a cell, this function returns a Vector3 that is 
    // centered on that cell.
    public Vector3 SnapToGrid(Vector3 worldPos)
    {
        return Cart2World(World2Cart(worldPos));
    }

    // given a world position that is contained within a cell, this function returns a Vector3 that is 
    // centered on that cell.
    public Vector3 SnapToGrid(int index)
    {
        return Cart2World(Index2Cart(index));

    }

    public Vector3 SnapToGrid(int x, int y)
    {
        return Cart2World(x,y);

    }

    // not the best way to do this, but for now (maybe forever)
    // this returns a list of floats that make up the worldspace boundaries
    // of the grid
    // [0] = min X
    // [1] = max X
    // [2] = min Y
    // [3] = max Y
    public List<float> CalculateBoundaries()
    {
        Vector3 bottomLeftCorner = SnapToGrid(0, 0);
        bottomLeftCorner.x -= cellOffset.x;
        bottomLeftCorner.y -= cellOffset.y;

        Vector3 topRightCorner = SnapToGrid(cols - 1, rows - 1);
        topRightCorner.x += cellOffset.x;
        topRightCorner.y += cellOffset.y;

        return new List<float>() { bottomLeftCorner.x, topRightCorner.x, bottomLeftCorner.y, topRightCorner.y };
    }

    public List<List<Vector3>> CalculateGridLines()
    {
        Vector3 bottomLeftCorner = SnapToGrid(0, 0);
        bottomLeftCorner.x -= cellOffset.x;
        bottomLeftCorner.y -= cellOffset.y;

        Vector3 topRightCorner = SnapToGrid(cols - 1, rows - 1);
        topRightCorner.x += cellOffset.x;
        topRightCorner.y += cellOffset.y;

        List<List<Vector3>> lines = new List<List<Vector3>>();
        
        for(int i = 0; i < Cols; i++)
        {
            float x = bottomLeftCorner.x + (i * cellScale.x);
            Vector3 start = new Vector3(x, bottomLeftCorner.y);
            Vector3 end = new Vector3(x, topRightCorner.y);
            lines.Add(new List<Vector3>() { start, end });
        }

        for(int i = 0; i < Rows; i++)
        {
            float y = bottomLeftCorner.y + (i * cellScale.y);
            Vector3 start = new Vector3(bottomLeftCorner.x, y);
            Vector3 end = new Vector3(topRightCorner.x, y);
            lines.Add(new List<Vector3>() { start, end });
        }
        return lines;
    }
}
