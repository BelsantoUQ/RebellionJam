using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnRandomCards : MonoBehaviour
{

    [SerializeField] private List<GameObject> cards;
    [SerializeField] private List<Vector3> spawnPositions;
    public List<int> currentCardsPositions;
    private GameManager gameManager;
    
    private void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager.IsContinuing)
        {
            InstantiateCuntinuePosition();
        }
        else
        {
            InstantiateOnRandomPosition();
        }
    }

    void InstantiateCuntinuePosition()
    {
        int index = 0;
        currentCardsPositions = new List<int>();
        foreach (var card in cards)
        {
            card.GetComponent<Card>().cardIndex = index;
            GameObject newCard = Instantiate(card, spawnPositions[gameManager.CurrentCardsPositions[index].Position], card.transform.rotation);
            currentCardsPositions.Add(gameManager.CurrentCardsPositions[index].Position);
            spawnPositions.RemoveAt(gameManager.CurrentCardsPositions[index].Position);
            if (!gameManager.CurrentCardsPositions[index].IsPositionActive)
            {
                newCard.SetActive(false);
                Debug.Log("Se desactiva la carta "+ index);
            }
            index++;
        }

    }

    void InstantiateOnRandomPosition()
    {
        int index = 0;
        currentCardsPositions = new List<int>();
        foreach (var card in cards)
        {
            int randomPos = Random.Range(0, spawnPositions.Count);
            card.GetComponent<Card>().cardIndex = index;
            Instantiate(card, spawnPositions[randomPos], card.transform.rotation);
            gameManager.CurrentCardsPositions.Add(new CardPosition());
            gameManager.CurrentCardsPositions[index].Position = randomPos;
            gameManager.CurrentCardsPositions[index].IsPositionActive = true;
            spawnPositions.RemoveAt(randomPos);
            index++;
        }

         
    }

}
