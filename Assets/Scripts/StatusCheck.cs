using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanzo.Player;

public class StatusCheck : MonoBehaviour
{
    private PlayerScript playerScript;
    private bool isPlaying;
    public bool IsPlaying
    {
        get { return isPlaying; }
        set { isPlaying = value; }
    }

    private void Start()
    {
        playerScript = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.enabled == false)
        {
            IsPlaying = true;

        }

    }
}
