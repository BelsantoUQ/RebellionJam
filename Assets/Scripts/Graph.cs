using System;
using System.Collections.Generic;

public class Graph
{
    private List<Node> nodes;

    public Graph(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    public List<int> FindShortestPath(int startNodeId, int endNodeId, int[,] matrizDistancias)
    {
        // Implementa aquí el algoritmo de Dijkstra para encontrar el camino más corto.
        // Retorna una lista de nodos que representa el camino desde el nodo de inicio hasta el nodo de destino.
        return new List<int>();
    }

    public void AddNeighborsFromAdjacencyMatrix(int[,] adjacencyMatrix)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            Node currentNode = nodes[i];

            for (int j = 0; j < nodes.Count; j++)
            {
                if (adjacencyMatrix[i, j] == 1)
                {
                    Node neighborNode = nodes[j];
                    currentNode.AddNeighbor(neighborNode.NodeId);
                    //Debug.Log("Vecino "+neighborNode.NodeId +" añadido en "+currentNode.NodeId);
                }
            }
        }
    }
    
}