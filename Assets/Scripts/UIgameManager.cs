using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIgameManager : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenu;
    private bool pauseMenuPressed = false;

    private void Update()
    {
        PauseGame();
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenuPressed)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            pauseMenuPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuPressed)
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            pauseMenuPressed = false;
        }
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
