using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Hanzo;

public class PlayerCounter : MonoBehaviour
{
    public TextMeshProUGUI playerCount;
    public int maxPlayers;

    GameObject[] players;
    Timer timer;

    private void Start()
    {
        timer = FindAnyObjectByType<Timer>();
        maxPlayers = timer.numberOfplayers;
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        playerCount.text = $"{players.Length}/{maxPlayers}";

    }
}
