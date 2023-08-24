using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public Transform playerContainer;


    // Start is called before the first frame update
    void Start()
    {
        GameObject player = PhotonNetwork.Instantiate(PlayerPrefab.name, transform.position, Quaternion.identity);
        player.transform.SetParent(playerContainer);
    }


}
