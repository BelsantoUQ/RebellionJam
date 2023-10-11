using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator; // Agrega una referencia al Animator
    private int currentI = 0;
    private int currentJ = 1;
    private bool isMoving = false;
    private float currentCooldown = 0f;
    private int currentNode = 2;
    private float speed = 0;
    
    [SerializeField]
    private float moveCooldown = 0.5f; // Tiempo de espera entre movimientos
    [SerializeField]
    private float walkSpeed = 2.0f; // Velocidad de caminar
    [SerializeField]
    private float runSpeed = 6.0f;  // Velocidad de correr
    //int destinationNodeId = 20;
    //private List<int> shortestPath;
    //private int currentPathIndex = 0; // Variable para rastrear el índice actual en el camino
    
    
    private bool nearCard;


    
    // Declarar e inicializar la matriz de Nodos en el mapa la cual contiene
    // un id de nodo, un Vector3 (posicion) y un entero que indica 0 si está diponible 
    // (es decir que es accesible) o 1 si el jugador esta parado sobre ese nodo
    // contructor del objeto: public Node(int nodeId, float x, float z, int isAble = 0)
    List<Node> nodeList = new List<Node>();
    Node[,] nodos = new Node[5,5]
    {
        {new Node(1,4.25f,-4.5f), new Node(2,4.25f,-2f,1), new Node(3,4.25f,.5f), new Node(4,4.25f,3.06f), new Node(5,4.25f,5.5f)},
        {new Node(6,0.92f,-4.5f), new Node(7,0.92f,-2f), new Node(8,0.92f,.5f), new Node(9,0.92f,3.06f), new Node(10,0.92f,5.5f)},
        {new Node(11,-2.74f,-4.5f), new Node(12,-2.74f,-2f), new Node(13,-2.74f,.5f), new Node(14,-2.74f,3.06f), new Node(15,-2.74f,5.5f)},
        {new Node(16,-6.4f,-4.5f), new Node(17,-6.4f,-2f), new Node(18,-6.4f,.5f), new Node(19,-6.4f,3.06f), new Node(20,-6.4f,5.5f)},
        {new Node(21,-10.2f,-4.5f), new Node(22,-10.2f,-2f), new Node(23,-10.2f,.5f), new Node(24,-10.2f,3.06f), new Node(25,-10.2f,5.5f)}
    };
    
    int[,] matrizDeAdyacencia = new int[25,25]
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
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0}, 
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0}, 
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0}, 
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0}, 
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0}, 
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0}, 
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0}, 
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0}, 
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1}, 
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 }
    };

    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Obtiene la referencia al Animator
        AddNodes();
        AddNeighborsFromAdjacencyMatrix(matrizDeAdyacencia); // Agregar vecinos desde la matriz de adyacencia
        agent.SetDestination(nodos[currentI, currentJ].Position); // Inicialmente, mueve al jugador al primer nodo

        // Recuerda actualizar "currentNode" cuando haya terminado el recorrido para seguir la posición actual del personaje.
        
        // Comienza a mover al personaje hacia el primer nodo del camino más corto
        
    }
    // Update is called once per frame
    void Update()
    {
        
        // Calcula la velocidad actual del NavMeshAgent
        speed = agent.velocity.magnitude;
        //Debug.Log("speed: "+speed);
        if (speed>0.5f)
        {
            // Establece el valor del parámetro "Walk" en el Animator
            animator.SetFloat("Walk", speed > 1.8f ? 1 : 0.3f);
            
        }
        else
        {
            animator.SetFloat("Walk", 0);
        }
        

        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W) && currentI < 4)
            {
                TryMove(currentI + 1, currentJ);
            }
            else if (Input.GetKeyDown(KeyCode.A) && currentJ > 0)
            {
                TryMove(currentI, currentJ - 1);
            }
            else if (Input.GetKeyDown(KeyCode.S) && currentI > 0)
            {
                TryMove(currentI - 1, currentJ);
            }
            else if (Input.GetKeyDown(KeyCode.D) && currentJ < 4)
            {
                TryMove(currentI, currentJ + 1);
            }
        }

        // Reduce el tiempo de espera entre movimientos
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    private void TryMove(int newI, int newJ)
    {
        if (nodos[currentI, currentJ].HasNeighborWithId(nodos[newI, newJ].NodeId) && currentCooldown <= 0)
        {
            isMoving = true;
            currentI = newI;
            currentJ = newJ;
            agent.SetDestination(nodos[currentI, currentJ].Position);
            StartCoroutine(MoveCooldown());
        }
    }

    private IEnumerator MoveCooldown()
    {
        yield return new WaitForSeconds(moveCooldown);
        isMoving = false;
        currentNode = nodos[currentI, currentJ].NodeId;
        currentCooldown = moveCooldown;
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
                nodeList.Add(nodos[i, j]); // Agregar cada nodo a la lista
            }
        }
    }





}
