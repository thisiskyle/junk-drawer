using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] 
public class SerializableVector2
{
    public float x;
    public float y;

    public SerializableVector2(float _x, float _y) 
    {
        x = _x;
        y = _y;
    }
    public SerializableVector2(int _x, int _y) 
    {
        x = (float)_x;
        y = (float)_y;
    }
    public override string ToString()
    {
        return $"({x}, {y})";
    }
    public static implicit operator Vector2(SerializableVector2 v)
    {
        return new Vector2(v.x, v.y);
    }
    public static implicit operator SerializableVector2(Vector2 v)
    {
        return new SerializableVector2(v.x, v.y);
    }
}
