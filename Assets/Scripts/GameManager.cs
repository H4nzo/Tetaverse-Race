using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;


public class GameManager : MonoBehaviour
{
    [SerializeField]GameObject mainWindow, loginPanel;
    

    public void Play(string level)
    {
        PhotonNetwork.LoadLevel(level);
    }


    public void Quit()
    {
        Application.Quit();
    }


     public void LogoutButton()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        loginPanel.SetActive(true);
        mainWindow.SetActive(false);
        string name = null;
        string password = null;
        PlayerPrefs.SetString("VALID_EMAIL", name);
        PlayerPrefs.SetString("VALID_PASSWORD", password);
        

    }
}
