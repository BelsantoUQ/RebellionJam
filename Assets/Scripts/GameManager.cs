using System;
using UnityEngine;
using System.IO;

[Serializable]
public class GameManagerData
{
    public bool IsMia;
    public bool IsOldPlayer;
    public int CurrentNode;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private const string fileName = "gameManagerData.json";
    private string savePath;
    private bool isContinuing= false;
    private bool isMia;
    private bool isOldPlayer;
    private int currentNode;

    public bool IsMia
    {
        get { return isMia; }
        set { isMia = value; }
    }

    public bool IsContinuing
    {
        get { return isContinuing; }
        set { isContinuing = value; }
    }

    public bool IsOldPlayer
    {
        get { return isOldPlayer; }
        set { isOldPlayer = value; }
    }

    public int CurrentNode
    {
        get { return currentNode; }
        set { currentNode = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        savePath = Path.Combine(Application.persistentDataPath, fileName);
        LoadData(); // Load data on Awake
    }

    public void SaveData()
    {
        GameManagerData data = new GameManagerData
        {
            IsMia = isMia,
            IsOldPlayer = isOldPlayer,
            CurrentNode = currentNode
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }

    public void LoadData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            GameManagerData data = JsonUtility.FromJson<GameManagerData>(json);

            isMia = data.IsMia;
            isOldPlayer = data.IsOldPlayer;
            currentNode = data.CurrentNode;
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}