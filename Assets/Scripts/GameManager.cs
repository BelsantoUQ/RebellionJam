using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Static reference to the GameManager instance
    public static GameManager Instance { get; private set; }
    
    // Other GameManager variables and functions
    public bool IsMia { get; set; }

    private void Awake()
    {
        // Check if an instance of GameManager already exists
        if (Instance == null)
        {
            // If not, set the instance to this and mark it as persistent across scenes
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this GameManager to enforce the Singleton pattern
            Destroy(gameObject);
        }
    }
}