using System;
using UnityEngine;

public class Node : IComparable
{
    public float nodeTotalCost;
    public float estimatedCost;
    public bool bObstacle;
    public Node parent;
    public Vector3 position;

    public static readonly Node Empty = null;

    public Node() : this(Vector3.zero)
    {
        //this.estimatedCost = 0f;
        //this.nodeTotalCost = 1.0f;
        //this.bObstacle = false;
        //this.parent = null;
    }

    public Node(Vector3 pos)
    {
        this.estimatedCost = 0f;
        this.nodeTotalCost = 1.0f;
        this.bObstacle = false;
        this.parent = null;
        this.position = pos;
    }

    public void MarkAsObstacle()
    {
        this.bObstacle = true;
    }

    public int CompareTo(object obj)
    {
        Node node = (Node)obj;
        if (this.estimatedCost < node.estimatedCost)
            return -1;
        else if (this.estimatedCost > node.estimatedCost)
            return 1;        
        else
            return 0;
    }
}
