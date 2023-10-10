using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCompare : MonoBehaviour
{


    public string firstCardTag = null;
    public string secondCardTag = null;

    public int firstCardInt;
    public int secondCardInt;

    private GameObject[] correctCards = new GameObject[2];

    private void Update()
    {

    }


    public void AssignCardTag(GameObject card)
    {
        string cardTag = card.tag;
        if (firstCardTag == null)
        {
            StartCoroutine(card.GetComponent<Card>().FlipAnimation());
            firstCardTag = cardTag;
            Debug.Log(firstCardTag);
            correctCards[0] = card;
        }
        else
        {
            StartCoroutine(card.GetComponent<Card>().FlipAnimation());

            secondCardTag = cardTag;
            Debug.Log(secondCardTag);
            correctCards[1] = card;

            CompareCards(cardTag);
        }

        //if (firstCardTag == secondCardTag)
        //{
        //    correctCards = GameObject.FindGameObjectsWithTag(firstCardTag);
        //    foreach (var card in correctCards)
        //    {
        //        card.gameObject.GetComponent<CardOnClick>().DeactivateIfQual();
        //    }

        //}
        //firstCardTag = null;
        //secondCardTag = null;

    }


    public void CompareCards(string cardTag)
    {
        if (firstCardTag == secondCardTag)
        {
            Debug.Log("correcto");

            //correctCards = GameObject.FindGameObjectsWithTag(firstCardTag);
            foreach (var card in correctCards)
            {
                card.gameObject.GetComponent<Card>().DeactivateIfQual();
            }
            firstCardTag = null;
            secondCardTag = null;
        }
        else
        {
            firstCardTag = null;
            secondCardTag = null;
            Debug.Log("diferente");
        }
    }

}
