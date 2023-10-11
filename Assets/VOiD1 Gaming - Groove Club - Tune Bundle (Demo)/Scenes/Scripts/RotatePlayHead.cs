using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayHead : MonoBehaviour
{

    public float rotspeed;
    public bool movingleft;

    private void FixedUpdate()
    {
        if (transform.rotation.eulerAngles.z <= 2f)
        {

            movingleft = true;
        }
        else if (transform.rotation.eulerAngles.z >= 7f)
        {
            movingleft = false;

        }

        if (movingleft)
        {
            transform.Rotate(new UnityEngine.Vector3(0f, 0f, -rotspeed));

        }
        else
        {
            transform.Rotate(new UnityEngine.Vector3(0f, 0f, rotspeed));

        }
    }
}
