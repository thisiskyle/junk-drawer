using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid3D<T> : IGrid
{
    private Vector3 dimensions;
    private Vector3 cellScale;
    private Vector3 offsetFromZero;
    private T[] cells;

    public int Cols { get { return (int)dimensions.x; } private set {} }
    public int Rows { get { return (int)dimensions.y; } private set {} }
    public int Steps { get { return (int)dimensions.z; } private set {} }


    public Grid3D(int cols, int rows, int steps, Vector3 cellScale, Vector3 offsetFromZero)
    {
        dimensions = new Vector3((float)cols, (float)rows, (float)steps);
        this.offsetFromZero = offsetFromZero;
        this.cellScale = cellScale;
        cells = new T[cols * rows * steps];
    }

    public Grid3D(int cols, int rows, int steps)
    {
        dimensions = new Vector3((float)cols, (float)rows, (float)steps);
        this.offsetFromZero = Vector3.zero;
        this.cellScale = new Vector3(1,1,1);
        cells = new T[cols * rows * steps];
    }

    public int Cart2Index(int x, int y, int z)
    {
        return z + y * (Cols * Steps) + (Rows * Steps);
    }

    public int Cart2Index(Vector3 pos)
    {
        return (int)pos.z + (int)pos.y * (Cols * Steps) + (Rows * Steps);
    }
    
    public Vector3 Index2Cart(int index)
    {
        int y = Mathf.FloorToInt(index / (Cols * Steps);
        int x = Mathf.FloorToInt(index / (y * (Cols * Steps) + Steps);
        int z = Mathf.FloorToInt(index - (y * (Cols * Steps)) + (x * Steps));
        return new Vector3(x, y, z);
    }
}
