using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    [SerializeField] private GameObject cardCompare;
    [SerializeField] private GameObject cardFlip;

    public int cardIndex;

    private bool cardDeactivated = false;
    private bool playerNear;

    private void Start()
    {
        cardCompare = GameObject.Find("CardCompare");
    }


    private void Update()
    {
        cardSelected();
    }


    private void OnTriggerEnter(Collider other)
    {
        playerNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerNear = false;
    }


    public void cardSelected()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.Space)) 
        {
            cardCompare.GetComponent<CardCompare>().AssignCardTag(gameObject);
        
        }
    }

    public void FlipAnimation()
    {
        cardFlip.GetComponent<CardFlip>().DotTweenFlipAnimation();

    }

    public void returnToInitialPosition()
    {
        cardFlip.GetComponent<CardFlip>().cardIsFlip = false;
        cardFlip.GetComponent<CardFlip>().ResetFlip();
    }


    public IEnumerator FlipAnimationCo()
    {
        // Duración de la animación en segundos
        float tiempoDeDuracion = 0.5f;
        // Rotación final
        Vector3 nuevaRotacion = new Vector3(0, 180, 0);
        Vector3 rotacionInicial = transform.rotation.eulerAngles;

        float tiempoPasado = 0;
        if (!cardDeactivated)
        {
            while (tiempoPasado < tiempoDeDuracion)
            {
                tiempoPasado += Time.deltaTime;
                float t = tiempoPasado / tiempoDeDuracion;
                transform.rotation = Quaternion.Euler(Vector3.Lerp(rotacionInicial, nuevaRotacion, t));
                yield return null;
            }

            yield return new WaitForSeconds(1f);

            tiempoDeDuracion = 0.5f;
            tiempoPasado = 0;

            while (tiempoPasado < tiempoDeDuracion)
            {
                tiempoPasado += Time.deltaTime;
                float t = tiempoPasado / tiempoDeDuracion;
                transform.rotation = Quaternion.Euler(Vector3.Lerp(nuevaRotacion, rotacionInicial, t));
                yield return null;
            }

        }
    }



    public IEnumerator DeactivateIfQual()
    {
        yield return new WaitForSeconds (1f);
        transform.DOScale(0.1f, 1f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }


}
