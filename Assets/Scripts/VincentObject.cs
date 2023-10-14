using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VincentObject : SelectorCharacter
{
    
    [SerializeField]
    private MiaObject miaObject;
    private void OnMouseEnter()
    {
        if (!isSelected)
        {
            // Cuando el mouse entra en el objeto con el tag "Mia".
            //Debug.Log("Estoy parado sobre Mia");
            miaObject.HooverActive(false);
            HooverActive(true);
        }
    }
    
    
    private void OnMouseDown()
    {
        if (!isSelected)
        {
            // Cuando se hace clic en el objeto con el tag "Mia".
            //Debug.Log("Se le dio clic a Mia");
            //Iniciar el juego con mia
            miaObject.SetDeath();
            ownAnimator.SetTrigger("Nice");
            gameManager.IsMia = false;
            isSelected = true;
            PlayGame();
        }
    }
}