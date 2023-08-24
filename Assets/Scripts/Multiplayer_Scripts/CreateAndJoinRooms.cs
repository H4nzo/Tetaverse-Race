using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField createInputField;
    [SerializeField] InputField joinInputField;
    [SerializeField] byte maxPlayers = 1;
    [SerializeField] const string GAME_SCENE = "MainRemake";


    public void CreateRoom()
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = maxPlayers;
        PhotonNetwork.CreateRoom(createInputField.text, ro, TypedLobby.Default);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInputField.text);
    }
    public override void OnJoinedRoom()
    {
        
       PhotonNetwork.LoadLevel(GAME_SCENE);
    }


}

