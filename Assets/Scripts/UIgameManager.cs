using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIgameManager : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject smartphone;
    [SerializeField] private Slider volumeSlider;
    
    private bool pauseMenuPressed = false;

    private void Start()
    {
        volumeSlider.value = AudioManager.instance.backgroundMusic.volume;
    }

    private void Update()
    {
        PauseGame();
    }

    public void OnVolumeSliderChanged()
    {
        AudioManager.instance.SetBackgroundMusicVolume(volumeSlider.value);
    }
    
    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenuPressed)
        {
            smartphone.SetActive(true);
            pauseMenuPressed = true;
            StartCoroutine(WaitForPauseGame());
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuPressed)
        {
            Time.timeScale = 1f;
            smartphone.SetActive(false);
            pauseMenu.SetActive(false);
            pauseMenuPressed = false;
        }
    }


    public IEnumerator WaitForPauseGame()
    {
        yield return new WaitForSeconds(.3f);
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
    public void tryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void backToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
