using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanzo;
public class Playmaker : MonoBehaviour
{
    Timer _timer;
    public StatusCheck[] statusChecks;

    // Update is called once per frame
    void Update()
    {
        statusChecks = GameObject.FindObjectsOfType<StatusCheck>();

        if (statusChecks.Length >= 3)
        {
            if (statusChecks[0].isDead == true)
            {
                _timer = GameObject.FindObjectOfType<Timer>();
                //All Players are dead  && statusChecks[1].isDead == true && statusChecks[2].isDead == true
                _timer.OnGameOver();
            }
        }
        





    }
}
