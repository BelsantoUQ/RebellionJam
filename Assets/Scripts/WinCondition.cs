using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{

    [SerializeField] private GameObject compareCards;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject smartphone;

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
        smartphone.SetActive(true);
        Debug.Log("Winnerrrrr");
    }








}
