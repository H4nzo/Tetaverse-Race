using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanzo.Player;
using Hanzo;

public class StatusCheck : MonoBehaviour
{
    public PlayerScript playerScript;
    public bool isDead = false;

    // Update is called once per frame
    void Update()
    {
        if (isDead == true)
        {
            playerScript.enabled = false;
            Timer _timer = GameObject.FindObjectOfType<Timer>();
            _timer.OnGameOver();
            gameObject.tag = "Untagged";
        }

    }
}
