using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("LoginScene");
        Time.timeScale = 1f;
    }

    public void GotoLobby()
    {
        SceneManager.LoadScene("Lobby");
        Time.timeScale = 1f;
    }

}
