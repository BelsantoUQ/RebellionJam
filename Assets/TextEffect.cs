using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    public float hoverIntensity = 1.5f; // Intensidad del brillo al hacer hover.
    public float clickBlinkSpeed = 5.0f; // Velocidad de parpadeo al hacer clic.

    [SerializeField] private GameObject bg;


    private Text text;
    private Color originalColor;
    private bool isHovered;
    private bool isClicked;
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
}