using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCompare : MonoBehaviour
{

    public delegate void AllcardsMatchedHandler();
    public event AllcardsMatchedHandler AllcardsMatchedEvent;
    private int cardsMatched = 0;

    public string firstCardTag = "";
    public string secondCardTag = "";

    public int firstCardIndex = -1;
    public int secondCardIndex = -1;

    private GameObject[] correctCards = new GameObject[2];



    public void AssignCardTag(GameObject card)
    {
        string cardTag = card.tag;
        int cardIndex = card.GetComponent<Card>().cardIndex;
        if (firstCardTag == "")
        {
            //StartCoroutine(card.GetComponent<Card>().FlipAnimationCo());
            card.GetComponent<Card>().FlipAnimation();


            firstCardTag = cardTag;
            firstCardIndex = cardIndex;
            correctCards[0] = card;
        }
        else
        {
            //StartCoroutine(card.GetComponent<Card>().FlipAnimationCo());
            card.GetComponent<Card>().FlipAnimation();


            secondCardTag = cardTag;
            secondCardIndex = cardIndex;
            correctCards[1] = card;

            CompareCards();
        }

    }


    public void CompareCards()
    {
        if (firstCardTag == secondCardTag && firstCardIndex != secondCardIndex)
        {

            //correctCards = GameObject.FindGameObjectsWithTag(firstCardTag);
            foreach (var card in correctCards)
            {
                StartCoroutine(card.gameObject.GetComponent<Card>().DeactivateIfQual());
            }
            firstCardTag = "";
            secondCardTag = "";

            firstCardIndex = -1;
            secondCardIndex = -1;
            cardsMatched++;
            if (cardsMatched == 6)
            {
                OnAllCardsMatched();
            }
        }
        else
        {
            StartCoroutine(returnPosition());
            firstCardTag = "";
            secondCardTag = "";

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


    IEnumerator returnPosition()
    {
        yield return new WaitForSeconds(1f);
        if (correctCards[0] != null && correctCards[1] != null)
        {
            foreach (var card in correctCards)
            {
                card.gameObject.GetComponent<Card>().returnToInitialPosition();
            }
        }
    }



}
