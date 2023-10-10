using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int NodeId { get; set; }
    public Vector3 Position { get; set; }
    public int IsAble { get; set; }

    private List<int> Neighbors; 

    // Constructor para inicializar un objeto MyObject con valores iniciales.
    public Node(int nodeId, float x, float z, int isAble = 0)
    {
        NodeId = nodeId;
        Position = CreateVector3(x, z);
        IsAble = isAble;
        Neighbors = new List<int>();
    }

    // Método para crear un Vector3 a partir de tres valores flotantes.
    public Vector3 CreateVector3(float x, float z, float y = 0.9f)
    {
        return new Vector3(x, y, z);
    }

    public bool HasNeighborWithId(int nodeId)
    {
        return Neighbors.Contains(nodeId);
    }
    
    public List<int> GetNeighbors()
    {
        return Neighbors;
    }
    
    public bool AddNeighbor(int nodeNeigborId)
    {
        if (nodeNeigborId != NodeId)
        {
            Neighbors.Add(nodeNeigborId);
        }

        return nodeNeigborId != NodeId;
    }
    public void PrintNeighbors()
    {
        Debug.Log("Neighbors of Node " + NodeId + ":");
        foreach (int neighborId in Neighbors)
        {
            Debug.Log(neighborId);
        }
    }
}