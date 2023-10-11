using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCompare : MonoBehaviour
{

    public delegate void AllcardsMatchedHandler();
    public event AllcardsMatchedHandler AllcardsMatchedEvent;
    private int cardsMatched = 0;

    public string firstCardTag = null;
    public string secondCardTag = null;

    public int firstCardIndex;
    public int secondCardIndex;

    private GameObject[] correctCards = new GameObject[2];

    private void Update()
    {

    }


    public void AssignCardTag(GameObject card)
    {
        string cardTag = card.tag;
        int cardIndex = card.GetComponent<Card>().cardIndex;
        if (firstCardTag == null)
        {
            StartCoroutine(card.GetComponent<Card>().FlipAnimation());

            firstCardTag = cardTag;
            firstCardIndex = cardIndex;
            correctCards[0] = card;
        }
        else
        {
            StartCoroutine(card.GetComponent<Card>().FlipAnimation());

            secondCardTag = cardTag;
            secondCardIndex = cardIndex;
            correctCards[1] = card;

            CompareCards(cardTag);
        }

    }


    public void CompareCards(string cardTag)
    {
        if (firstCardTag == secondCardTag && firstCardIndex != secondCardIndex)
        {
            Debug.Log("correcto");

            //correctCards = GameObject.FindGameObjectsWithTag(firstCardTag);
            foreach (var card in correctCards)
            {
                card.gameObject.GetComponent<Card>().DeactivateIfQual();
            }
            firstCardTag = null;
            secondCardTag = null;

            firstCardIndex = -1;
            secondCardIndex = -1;
            cardsMatched++;
            if (cardsMatched == 1)
            {
                OnAllCardsMatched();
            }
        }
        else
        {
            firstCardTag = null;
            secondCardTag = null;

            firstCardIndex = -1;
            secondCardIndex = -1;
            Debug.Log("diferente");
        }
    }

    protected void OnAllCardsMatched()
    {
        AllcardsMatchedHandler handler = AllcardsMatchedEvent;

        if (handler != null)
        {
            handler();
        }

    }



}
