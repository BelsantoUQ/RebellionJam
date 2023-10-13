using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlip : MonoBehaviour
{


    public bool cardIsFlip = false;



    [ContextMenu("flip tween")]
    public void DotTweenFlipAnimation()
    {

        if (!cardIsFlip)
        {
            transform.DORotate(Vector3.Scale(new Vector3(0, 1, 90), new Vector3(0, 90, 1)), 1f);
            cardIsFlip = true;
        }
        //else
        //{
        //    transform.DORotate(Vector3.Scale(new Vector3(0, 1, 90), new Vector3(0, -90, 1)), 1f);
        //    cardIsFlip = false;
        //}



    }


    public void ResetFlip()
    {
        transform.DORotate(Vector3.Scale(new Vector3(0, 1, 90), new Vector3(0, -90, 1)), 1f);

    }


}
