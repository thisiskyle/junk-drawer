using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AStar
{

    // TODO: Pretty sure this needs to a reference to the grid that this is looking for a path on
    public List<Node> FindPath(Node startNode, Node targetNode)
    {
        
        List<Node> openSet = new List<Node>();
        List<Node> closedSet = new List<Node>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {

            Node currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if ((openSet[i].fcost < currentNode.fcost) || (openSet[i].fcost == currentNode.fcost)
                    && (openSet[i].hcost < currentNode.hcost))
                {
                    currentNode = openSet[i];
                }
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    return RetracePath(startNode, targetNode);
                }
            }
            foreach (Node node in GetNeighbors(currentNode))
            {
                if (!node.isWalkable || closedSet.Contains(node)) continue;

                float newCost = currentNode.gcost + GetDistance(currentNode, node);
                if ((newCost < node.gcost) || !openSet.Contains(node))
                {
                    node.SetG(newCost);
                    node.SetH(GetDistance(node, targetNode));
                    node.SetParent(currentNode);

                    if (!openSet.Contains(node))
                    {
                        openSet.Add(node);
                    }
                }
            }
        }
        return null;
    }
    private List<Node> RetracePath(Node startNode, Node targetNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = targetNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    float GetDistance(Node nodeA, Node nodeB)
    {
        float dX = Mathf.Abs(nodeA.gridpos.x - nodeB.gridpos.x);
        float dY = Mathf.Abs(nodeA.gridpos.y - nodeB.gridpos.y);

        if (dX > dY) { return dY + 10 * (dX - dY); }
        return dX + 10 * (dY - dX);
    }
    private List<Node> GetNeighbors(Node node) 
    {
        // TODO: Get neighbors here
        List<Node> neighbors = new List<Node>();
        return neighbors;
    }
}
