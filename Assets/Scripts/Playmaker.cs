using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanzo;
public class Playmaker : MonoBehaviour
{
    Timer _timer;
    private StatusCheck[] statusChecks;

    // Update is called once per frame
    void Update()
    {
        statusChecks = GameObject.FindObjectsOfType<StatusCheck>();

        if (statusChecks.Length >= 3)
        {
            if (statusChecks[0].IsPlaying == true && statusChecks[1].IsPlaying == true && statusChecks[2].IsPlaying == true)
            {
                //All Players are dead
                _timer.OnGameOver();
            }
        }
        _timer = GameObject.FindObjectOfType<Timer>();





    }
}
