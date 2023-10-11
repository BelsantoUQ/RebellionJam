using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class RotateDisc : MonoBehaviour
{
    public float rotspeed;
    private void FixedUpdate()
    {
        transform.Rotate(new UnityEngine.Vector3(0f,0f,rotspeed));

    }
}
