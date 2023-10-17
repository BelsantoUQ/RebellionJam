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
    private bool isControlAble = false;
    private float timeToInit = 0f;
    private float currentCooldown = 0f;
    private int currentNode = 2;
    private float speed = 0;
    private Graph world;
    private GameManager gameManager;
    [SerializeField]
    private float stopAnimationDistance = 0.1f; // Distancia a la que se detendrá la animación
    [SerializeField]
    private float moveCooldown = 0.5f; // Tiempo de espera entre movimientos

    [SerializeField] private bool isVincent;
    //int destinationNodeId = 20;
    //private List<int> shortestPath;
    //private int currentPathIndex = 0; // Variable para rastrear el índice actual en el camino
    
    
    private bool nearCard;


    
    

    
    void Start()
    {
        gameManager = GameManager.Instance;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Obtiene la referencia al Animator
        world = new Graph();
        // Inicialmente, mueve al jugador al primer nodo
        currentNode = gameManager.IsContinuing ? gameManager.CurrentNode : 2;
        var result = world.GetNodeById(currentNode);
        currentI = result.Item2;
        currentJ = result.Item3;
//        Debug.Log(currentNode+"Nodo: "+ gameManager.CurrentNode + "Pos i:"+currentI+ " j:"+currentJ);
        gameObject.transform.position =  (world.Nodos[currentI, currentJ].Position); 
            
        if (isVincent)
        {
            gameObject.SetActive(!gameManager.IsMia);
        }
        else
        {
            gameObject.SetActive(gameManager.IsMia);
        }
        
    }
    // Update is called once per frame
    private void Update()
    {

        if (isControlAble)
        {


            // Calcula la velocidad actual del NavMeshAgent
            speed = agent.velocity.magnitude;
            //Debug.Log("speed: "+speed);
            if (speed > 0.5f)
            {
                // Establece el valor del parámetro "Walk" en el Animator
                animator.SetFloat("Walk", speed > 1.8f ? 1 : 0.3f);

            }
            else
            {
                animator.SetFloat("Walk", 0);
            }


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
        else
        {
            timeToInit += Time.deltaTime;
//            Debug.Log("Time: "+timeToInit);
            if (timeToInit >46)
            {
                isControlAble = true;
            }
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
        gameManager.CurrentNode = world.Nodos[currentI, currentJ].NodeId;
        animator.SetFloat("Walk", 0); // Detener la animación al finalizar el movimiento
    }
}
