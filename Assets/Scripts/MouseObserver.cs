using UnityEngine;

public class MouseObserver : MonoBehaviour
{
    private void OnMouseEnter()
    {
        // Cuando el mouse entra en el objeto con el tag "Mia".
        Debug.Log("Estoy parado sobre Mia");
    }

    private void OnMouseDown()
    {
        // Cuando se hace clic en el objeto con el tag "Mia".
        Debug.Log("Se le dio clic a Mia");
    }
}