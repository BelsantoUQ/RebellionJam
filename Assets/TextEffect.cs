using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    [SerializeField] private float hoverIntensity = 1.5f; // Intensidad del brillo al hacer hover.
    [SerializeField] private float clickBlinkSpeed = 5.0f; // Velocidad de parpadeo al hacer clic.
    [SerializeField] private GameObject selector;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject bg;


    private Text text;
    private Color originalColor;
    private bool isHovered;
    private bool isClicked;
    private bool isActiveUI = true;
    private bool isActiveTitle = true;
    private float timeSinceClick;

    private void Awake()
    {
        text = GetComponent<Text>();
        originalColor = text.color;
    }

    private void Update()
    {
        if (isClicked)
        {
            timeSinceClick += Time.deltaTime;
            float lerpValue = Mathf.PingPong(timeSinceClick * clickBlinkSpeed, 1.0f);
            text.color = Color.Lerp(originalColor, Color.white, lerpValue);
        }
        else if (isHovered)
        {
            text.color = originalColor * hoverIntensity;
        }
        else
        {
            text.color = originalColor;
        }
    }

    public void OnPointerEnter()
    {
        bg.SetActive(true);
        isHovered = true;
    }

    public void OnPointerExit()
    {
        bg.SetActive(false);
        isHovered = false;
    }

    public void OnPointerClick()
    {
        isClicked = !isClicked;
        if (!isClicked)
        {
            timeSinceClick = 0f;
        }
    }

    
    public void OnInitGame()
    {
        StartCoroutine(InicioConRetraso());
    }
    
    public void OnShowCredits()
    {
        isActiveTitle = !isActiveTitle;
        if (isActiveTitle)
        {
            StartCoroutine(HideWithDelay());
        }
        else
        {
            title.SetActive(isActiveTitle);
        }
    }

    IEnumerator HideWithDelay()
    {
        // Espera n segundos
        yield return new WaitForSeconds(.5f);
        title.SetActive(isActiveTitle);

    }

    IEnumerator InicioConRetraso()
    {
        // Espera n segundos
        yield return new WaitForSeconds(1.2f);

        // Luego, ejecuta la función OnInitGame
        isActiveUI = !isActiveUI;
        ui.SetActive(isActiveUI);
        selector.SetActive(!isActiveUI);
    }
}