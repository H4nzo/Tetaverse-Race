using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    void Start()
    {
        Debug.Log("Connected to Photon Network");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        // PhotonNetwork.LoadLevel(1);
        SceneManager.LoadScene("Lobby");
        PhotonNetwork.NickName = PlayerPrefs.GetString("DISPLAYNAME");
    }



}
