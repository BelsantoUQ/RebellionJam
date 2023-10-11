using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowInteractable : MonoBehaviour
{

    [SerializeField] private GameObject spaceText;

    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = spaceText.transform.rotation;
    }

    private void Update()
    {
        spaceText.transform.rotation = initialRotation;
    }


    private void OnTriggerEnter(Collider other)
    {
        spaceText.SetActive(true);
    }


    private void OnTriggerExit(Collider other)
    {
        spaceText.SetActive(false);
    }



}
