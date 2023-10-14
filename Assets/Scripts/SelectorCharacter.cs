using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectorCharacter : MonoBehaviour
{
    
    [SerializeField]
    protected GameObject ownLight;
    protected Animator ownAnimator; // Agrega una referencia al Animator
    [SerializeField]
    protected  MeshRenderer rendererPanel;
    protected bool isSelected;
    protected Material material;
    //protected Color initialColor; // Change between FFFFFF if itsn't hoover and 000000 if it is
    protected GameManager gameManager;
    protected virtual void Start()
    {
        // Accede al GameManager utilizando la propiedad estática "Instance"
        gameManager = GameManager.Instance;
        //initialColor = rendererPanel.material.color;
        // Obtén el material una vez en Start para mejorar el rendimiento
        material = rendererPanel.material;
        //Debug.Log(material);
        //Debug.Log(initialColor);
        ownAnimator = GetComponent<Animator>(); // Obtiene la referencia al Animator
    }
    
    public void PlayGame()
    {
        StartCoroutine(WaitForPlayGame());
        gameManager.IsOldPlayer = true;
    }

    public IEnumerator WaitForPlayGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public virtual void SetDeath()
    {
        isSelected = true;
        ownAnimator.SetBool("GameOver", true);
    }

    public virtual void HooverActive(bool active)
    {
        ownLight.SetActive(active);
        ownAnimator.SetBool("Hoover", active);
        ChangeColor(active);
    }
    protected virtual void ChangeColor(bool targetColor)
    {
        // Cambia el color del material del rendererOwnPhone al color objetivo
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