using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    public float destroyTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
            Destroy(gameObject, destroyTime);
    }

    
}
