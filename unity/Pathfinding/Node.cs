using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool isWalkable = true;
    public Vector2 gridpos;
    public Vector3 worldpos;
    public float gcost { get; private set; }
    public float hcost { get; private set; }
    public float fcost { get { return gcost + hcost; } private set { }}
    public Node parent { get; private set; }
    

    public void SetParent(Node newParent) { parent = newParent; }
    public void SetG(float newG) { gcost = newG; }
    public void SetH(float newH) { hcost = newH; }
}
