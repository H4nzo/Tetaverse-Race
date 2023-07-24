using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ButtonManager : MonoBehaviour
{
    public Button lobbyBTN, homeBTN;
    // Start is called before the first frame update
    void Start()
    {
        lobbyBTN.onClick.AddListener(GotoLobby);
        homeBTN.onClick.AddListener(Home);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
     public void Home()
    {
       PhotonNetwork.LoadLevel("newMenu");
        Time.timeScale = 1f;
        // Debug.Log("Pressed");
    }

    
    public void GotoLobby()
    {
       PhotonNetwork.LoadLevel("Lobby");
    //    Debug.Log("Pressed");
        Time.timeScale = 1f;
    }

}
