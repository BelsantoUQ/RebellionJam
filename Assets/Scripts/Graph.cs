using System;
using System.Collections.Generic;

public class Graph
{
    private List<Node> nodes;
// Declarar e inicializar la matriz de Nodos en el mapa la cual contiene
    // un id de nodo, un Vector3 (posicion) y un entero que indica 0 si está diponible 
    // (es decir que es accesible) o 1 si el jugador esta parado sobre ese nodo
    // contructor del objeto: public Node(int nodeId, float x, float z, int isAble = 0)
    private List<Node> nodeList = new List<Node>();
    public Node[,] Nodos { get; private set; }

    public int[,] MatrizDeAdyacencia { get; private set; } 
    public Graph()
    {
        CrearNodos();
        CrearMatrizAdyacencia();
        AddNodes();
        AddNeighborsFromAdjacencyMatrix(MatrizDeAdyacencia);
    }

    private void CrearNodos()
    {
        Nodos = new Node[5,5]
        {
            {new Node(1,4.25f,-4.5f), new Node(2,4.25f,-2f,1), new Node(3,4.25f,.5f), new Node(4,4.25f,3.06f), new Node(5,4.25f,5.5f)},
            {new Node(6,0.92f,-4.5f), new Node(7,0.92f,-2f), new Node(8,0.92f,.5f), new Node(9,0.92f,3.06f), new Node(10,0.92f,5.5f)},
            {new Node(11,-2.74f,-4.5f), new Node(12,-2.74f,-2f), new Node(13,-2.74f,.5f), new Node(14,-2.74f,3.06f), new Node(15,-2.74f,5.5f)},
            {new Node(16,-6.4f,-4.5f), new Node(17,-6.4f,-2f), new Node(18,-6.4f,.5f), new Node(19,-6.4f,3.06f), new Node(20,-6.4f,5.5f)},
            {new Node(21,-10.2f,-4.5f), new Node(22,-10.2f,-2f), new Node(23,-10.2f,.5f), new Node(24,-10.2f,3.06f), new Node(25,-10.2f,5.5f)}
        };
    }

    private void CrearMatrizAdyacencia()
    {
        MatrizDeAdyacencia = new int[25,25]
        {         // 1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25
            /* 1 */ {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            /* 2 */ {1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            /* 3 */ {0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            /* 4 */ {0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            /* 5 */ {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            /* 6 */ {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            /* 7 */ {0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            /* 8 */ {0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            /* 9 */ {0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            /* 10*/ {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            /* 11*/ {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            /* 12*/ {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0},
            /* 13*/ {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            /* 14*/ {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            /* 15*/ {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 
            /* 16*/ {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0}, 
            /* 17*/ {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0}, 
            /* 18*/ {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0}, 
            /* 19*/ {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0}, 
            /* 20*/ {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0}, 
            /* 21*/ {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0}, 
            /* 22*/ {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0}, 
            /* 23*/ {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0}, 
            /* 24*/ {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1}, 
            /* 25*/ {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 }
        };
    }

    public List<int> FindShortestPath(int startNodeId, int endNodeId, int[,] matrizDistancias)
    {
        // Implementa aquí el algoritmo de Dijkstra para encontrar el camino más corto.
        // Retorna una lista de nodos que representa el camino desde el nodo de inicio hasta el nodo de destino.
        return new List<int>();
    }

    private void AddNeighborsFromAdjacencyMatrix(int[,] adjacencyMatrix)
    {
        for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < adjacencyMatrix.GetLength(1); j++)
            {
                if (adjacencyMatrix[i, j] == 1)
                {
                    // Si hay una conexión (valor 1) en la matriz de adyacencia entre los nodos i y j,
                    // agrega j como vecino de i y viceversa, ya que la matriz es simétrica en este caso.
                    nodeList[i].AddNeighbor(nodeList[j].NodeId);
                    nodeList[j].AddNeighbor(nodeList[i].NodeId);
                }
            }
        }
    }
    
    private void AddNodes()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                nodeList.Add(Nodos[i, j]); // Agregar cada nodo a la lista
            }
        }
    }
    
    public (Node, int, int) GetNodeById(int nodeId)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (Nodos[i, j].NodeId == nodeId)
                {
                    return (Nodos[i, j], i, j);
                }
            }
        }

        // Si no se encuentra ningún nodo con el ID proporcionado, devuelve nulo.
        return (null, -1, -1);
    }
}