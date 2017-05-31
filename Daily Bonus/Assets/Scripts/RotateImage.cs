using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateImage : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0,0,0.2f));
    }
}
