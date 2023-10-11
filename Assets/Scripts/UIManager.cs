using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider volumeSlider;

    [SerializeField] private GameObject credits;
    private bool creditsPressed = false;

    private void Start()
    {
        volumeSlider.value = AudioManager.instance.backgroundMusic.volume;
    }

    public void OnVolumeSliderChanged()
    {
        AudioManager.instance.SetBackgroundMusicVolume(volumeSlider.value);
    }

    public void playGame()
    {
        StartCoroutine(WaitForPlayGame());
    }

    public IEnumerator WaitForPlayGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void showCredits()
    {
        if (!creditsPressed)
        {
            credits.GetComponent<Animation>().Play("CreditIn");
            creditsPressed = true;
        } else
        {
            credits.GetComponent<Animation>().Play("Out");
            creditsPressed = false;
        }
    }


}