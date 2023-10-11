using UnityEngine;

public class MiaObject : MonoBehaviour
{
    [SerializeField]
    private GameObject lightMia;
    [SerializeField]
    private VincentObject playerVincent;
    private Animator animatorMia; // Agrega una referencia al Animator
    [SerializeField]
    private  MeshRenderer renderer;
    
    private Material material;
    private Color initialColor; // Change between FFFFFF if itsn't hoover and 000000 if it is
    private GameManager gameManager;
    private void Start()
    {
        // Accede al GameManager utilizando la propiedad estática "Instance"
        gameManager = GameManager.Instance;
        initialColor = renderer.material.color;
        // Obtén el material una vez en Start para mejorar el rendimiento
        material = renderer.material;
        Debug.Log(material);
        Debug.Log(initialColor);
        animatorMia = GetComponent<Animator>(); // Obtiene la referencia al Animator
    }
    
    private void OnMouseEnter()
    {
        // Cuando el mouse entra en el objeto con el tag "Mia".
        Debug.Log("Estoy parado sobre Mia");
        playerVincent.HooverActive(false);
        HooverActive(true);
    }

    private void OnMouseDown()
    {
        // Cuando se hace clic en el objeto con el tag "Mia".
        Debug.Log("Se le dio clic a Mia");
        //Iniciar el juego con mia
        playerVincent.SetDeath();
        animatorMia.SetTrigger("Nice");
        gameManager.IsMia = true;
    }
    public void SetDeath()
    {
        animatorMia.SetBool("GameOver", true);
    }

    public void HooverActive(bool active)
    {
        lightMia.SetActive(active);
        animatorMia.SetBool("Hoover", active);
        ChangeColor(active);
    }
    private void ChangeColor(bool targetColor)
    {
        // Cambia el color del material del renderer al color objetivo
        if (targetColor)//black
        {
            material.color = new Color(0, 0, 0, 0.5607843f);
        }
        else
        {
            material.color = new Color(1, 1, 1, 0.5607843f);
        }

    }

}