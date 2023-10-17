using System;
using System.Collections;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

[Serializable]
public class CardPosition
{
    public int Position { get; set; }
    public bool IsPositionActive { get; set; }
}

[Serializable]
public class GameManagerData
{
    public bool IsMia;
    public bool IsOldPlayer;
    public int CurrentNode;
    public int CardsMatched;
    public string CurrentCardsPositionString; // Cadena separada por el símbolo omega
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private const string fileName = "gameManagerData.json";
    private string savePath;
    private bool isContinuing = false;
    private bool isMia;
    private List<CardPosition> currentCardsPosition;
    private bool isOldPlayer;
    private int currentNode;
    private int cardsMatched;

    public List<CardPosition> CurrentCardsPositions
    {
        get { return currentCardsPosition; }
        set { currentCardsPosition = value; }
    }

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

    public int CardsMatched
    {
        get { return cardsMatched; }
        set { cardsMatched = value; }
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
            CurrentNode = currentNode,
            CardsMatched = cardsMatched,
        };

        // Convertir la lista a una cadena separada por el símbolo omega
        string positionsString = string.Join("Ω", currentCardsPosition.Select(card => $"{card.Position}:{card.IsPositionActive}").ToArray());

        data.CurrentCardsPositionString = positionsString;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }

    public void LoadData()
    {
        if (File.Exists(savePath))
        {
            try
            {
                string json = File.ReadAllText(savePath);
                GameManagerData data = JsonUtility.FromJson<GameManagerData>(json);

                isMia = data.IsMia;
                isOldPlayer = data.IsOldPlayer;
                currentNode = data.CurrentNode;
                cardsMatched = data.CardsMatched;

                if (!string.IsNullOrEmpty(data.CurrentCardsPositionString))
                {
                    // Convertir la cadena separada por el símbolo omega a una lista
                    string[] positionStrings = data.CurrentCardsPositionString.Split('Ω');
                    currentCardsPosition = new List<CardPosition>(positionStrings.Select(str =>
                    {
                        string[] parts = str.Split(':');
                        return new CardPosition
                        {
                            Position = int.Parse(parts[0]),
                            IsPositionActive = bool.Parse(parts[1])
                        };
                    }));
                }
                else
                {
                    // Si no hay datos guardados para CurrentCardsPositionString, inicializa la lista vacía
                    currentCardsPosition = new List<CardPosition>();
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error al cargar los datos: " + e.Message);
            }
        }
        else
        {
            Debug.Log("El archivo de guardado no existe.");
        }
    }

    public void NewCardsPosition()
    {
        currentCardsPosition = new List<CardPosition>();
    }
    
    public void PrintCurrentCardsPositions()
    {
        foreach (var card in currentCardsPosition)
        {
            Debug.Log($"Position: {card.Position}, IsPositionActive: {card.IsPositionActive}");
        }
    }

    private void OnApplicationQuit()
    {
        PrintCurrentCardsPositions();
        SaveData();
    }
}
