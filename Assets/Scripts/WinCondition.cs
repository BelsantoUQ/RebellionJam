using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{

    [SerializeField] private GameObject compareCards;
    [SerializeField] private GameObject winText;

    private void Start()
    {

        if (compareCards != null)
        {
            compareCards.GetComponent<CardCompare>().AllcardsMatchedEvent += HandleAllCardsMatchedEvent;
        }

    }


    private void HandleAllCardsMatchedEvent()
    {
        winText.SetActive(true);
        Debug.Log("Winnerrrrr");
    }








}
