using System;
using UnityEngine;

public class VincentObject : MonoBehaviour
{
    [SerializeField]
    private MiaObject playerMia;// Agrega una referencia al Animator
    [SerializeField]
    private GameObject lightVincent;
    private Animator animatorVincent; // Agrega una referencia al Animator
    
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
        animatorVincent = GetComponent<Animator>(); // Obtiene la referencia al Animator
    }

    private void OnMouseEnter()
    {
        // Cuando el mouse entra en el objeto con el tag "Mia".
        Debug.Log("Estoy parado sobre Vincent");
        //desactivar mia hoover
        playerMia.HooverActive(false);
        HooverActive(true);
    }

    private void OnMouseDown()
    {
        // Cuando se hace clic en el objeto con el tag "Mia".
        Debug.Log("Se le dio clic a Vincent");
        //Iniciar el juego con mia
        playerMia.SetDeath();
        animatorVincent.SetTrigger("Nice");
        gameManager.IsMia = false;
    }

    public void SetDeath()
    {
        animatorVincent.SetBool("GameOver", true);
    }

    public void HooverActive(bool active)
    {
        lightVincent.SetActive(active);
        animatorVincent.SetBool("Hoover", active);
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