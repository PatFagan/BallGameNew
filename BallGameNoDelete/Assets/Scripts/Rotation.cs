using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotX, rotY, rotZ;

    void Update()
    {
        transform.Rotate(rotX, rotY, rotZ, Space.World);
    }
}