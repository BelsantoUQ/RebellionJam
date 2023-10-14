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
        //verifica que el gameObject no sea nulo
        if (compareCards != null)
        {
            //Suscribe al evento
            compareCards.GetComponent<CardCompare>().AllcardsMatchedEvent += HandleAllCardsMatchedEvent;
        }

    }

    //Funcion que se ejecuta una vez ocurra el evento
    private void HandleAllCardsMatchedEvent()
    {
        winText.SetActive(true);
        smartphone.SetActive(true);
        Debug.Log("Winnerrrrr");
    }








}
