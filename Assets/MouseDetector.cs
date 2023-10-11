using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetector : MonoBehaviour
{
    public interface IMouseObserver
    {
        void OnMouseEnter();
        void OnMouseExit();
        void OnMouseClick();
    }
}
