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
    private Graph world;
    
    [SerializeField]
    private float stopAnimationDistance = 0.1f; // Distancia a la que se detendrá la animación
    [SerializeField]
    private float moveCooldown = 0.5f; // Tiempo de espera entre movimientos
  
    //int destinationNodeId = 20;
    //private List<int> shortestPath;
    //private int currentPathIndex = 0; // Variable para rastrear el índice actual en el camino
    
    
    
    

    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Obtiene la referencia al Animator
        world = new Graph();
        // Agregar vecinos desde la matriz de adyacencia
        agent.SetDestination(world.Nodos[currentI, currentJ].Position); // Inicialmente, mueve al jugador al primer nodo

        // Recuerda actualizar "currentNode" cuando haya terminado el recorrido para seguir la posición actual del personaje.
        
        // Comienza a mover al personaje hacia el primer nodo del camino más corto
        
    }
    // Update is called once per frame
    private void Update()
    {
        SetWalkAnimation();

        if (!isMoving)
        {
            // Realiza la rotación del personaje antes de moverlo
            if (Input.GetKeyDown(KeyCode.W) && currentI < 4)
            {
                if (speed < 0.1)
                    RotateCharacter(-90f);
                
                TryMove(currentI + 1, currentJ);
            }
            else if (Input.GetKeyDown(KeyCode.A) && currentJ > 0)
            {
                if (speed < 0.1)
                    RotateCharacter(180f);
                
                TryMove(currentI, currentJ - 1);
            }
            else if (Input.GetKeyDown(KeyCode.S) && currentI > 0)
            {
                if (speed < 0.1)
                    RotateCharacter(90f);
                
                TryMove(currentI - 1, currentJ);
            }
            else if (Input.GetKeyDown(KeyCode.D) && currentJ < 4)
            {
                if (speed < 0.1)
                    RotateCharacter(0f);
                
                TryMove(currentI, currentJ + 1);
            }
        }

        // Verifica si el jugador está cerca del destino y detiene la animación
        if (Vector3.Distance(transform.position, agent.destination) <= stopAnimationDistance)
        {
            animator.SetFloat("Walk", 0);
        }

        // Reduce el tiempo de espera entre movimientos
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }


    private void SetWalkAnimation()
    {
        // Calcula la velocidad actual del NavMeshAgent
        speed = agent.velocity.magnitude;
        if (speed < 0.1)
        {
            animator.SetFloat("Walk", 0);
        }
        else
        {
            // Establece el valor del parámetro "Walk" en el Animator
            animator.SetFloat("Walk", speed > 1.6f ? 1 : 0.3f);
        }
    }
    private void RotateCharacter(float yRotation)
    {
        // Rota el personaje en el eje Y
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    private void TryMove(int newI, int newJ)
    {
        if (world.Nodos[currentI, currentJ].HasNeighborWithId(world.Nodos[newI, newJ].NodeId) && currentCooldown <= 0)
        {
            isMoving = true;
            SetWalkAnimation();
            speed = .1f;
            currentI = newI;
            currentJ = newJ;
            agent.SetDestination(world.Nodos[currentI, currentJ].Position);
            StartCoroutine(MoveCooldown());
        }
    }

    private IEnumerator MoveCooldown()
    {
        yield return new WaitForSeconds(moveCooldown);
        isMoving = false;
        currentNode = world.Nodos[currentI, currentJ].NodeId;
        currentCooldown = moveCooldown;
        animator.SetFloat("Walk", 0); // Detener la animación al finalizar el movimiento
    }

    
    
   

}
