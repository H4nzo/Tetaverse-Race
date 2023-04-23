using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXDisabler : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

   
}
