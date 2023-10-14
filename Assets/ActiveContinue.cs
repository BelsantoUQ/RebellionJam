using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveContinue : MonoBehaviour
{
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
        if (!gameManager.IsOldPlayer)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        gameManager.IsContinuing = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
