using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCompare : MonoBehaviour
{
    //Delegado y evento
    public delegate void AllcardsMatchedHandler();
    public event AllcardsMatchedHandler AllcardsMatchedEvent;
    private int cardsMatched = 0;

<<<<<<< HEAD
    public string firstCardTag = "";
    public string secondCardTag = "";
=======
    //Variables para comparar las cartas
    public string firstCardTag = null;
    public string secondCardTag = null;
>>>>>>> main

    public int firstCardIndex = -1;
    public int secondCardIndex = -1;

    private GameObject[] correctCards = new GameObject[2];


    //Se le asigna un valor a las varibles de comparacion
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

<<<<<<< HEAD
            CompareCards();
=======
            //Una vez tenga las dos cartas compararlas para saber si son las mismas
            CompareCards(cardTag);
>>>>>>> main
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

            //Cuando haya resulto todas las combinaciones accione el evento
            if (cardsMatched == 1)
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

    //Funcion para activar el evento
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
