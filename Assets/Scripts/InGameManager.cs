using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void RedeemToken()
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
