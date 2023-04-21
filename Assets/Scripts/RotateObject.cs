using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    
    public float angularSpeed = 3f;
    
    void Update()
    {
        transform.Rotate(0f, angularSpeed, 0f);
    }
}
