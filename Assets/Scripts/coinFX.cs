using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinFX : MonoBehaviour
{
    [SerializeField] GameObject particleFX;
    [SerializeField] Transform FXContainer;

    MeshRenderer meshRenderer;

    private void Start()
    {
        FXContainer = GameObject.Find("FXContainer").transform;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            //Increment Score
            StartCoroutine(HandleFX(3f));

    }


    IEnumerator HandleFX(float t)
    {
        GameObject pfx = Instantiate(particleFX, transform.position, Quaternion.identity);
        meshRenderer.enabled = false;
        GetComponent<Collider>().enabled = false;
        pfx.transform.SetParent(FXContainer);

        yield return new WaitForSeconds(t);
        gameObject.SetActive(false);

    }


}

