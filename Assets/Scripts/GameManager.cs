using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject usernameWindow;
    [Header("Display Name Window")]
    public GameObject nameError;
    public TMP_InputField nameInput;

    [SerializeField] TextMeshProUGUI messageText;

    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void Play(string level)
    {
        PhotonNetwork.LoadLevel(level);
    }

    public void Sound()
    {
        //AudioListener should be toggled here...
    }


    public void LogoutButton()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        string name = null;
        string password = null;
        PlayerPrefs.SetString("VALID_EMAIL", name);
        PlayerPrefs.SetString("VALID_PASSWORD", password);

        FacebookAndPlayFabManager.Instance.LogOutFacebook();
        SceneManager.LoadScene("FirstLoginScene");
    }

    public void DisplayName()
    {
        usernameWindow.SetActive(true);
    }

    public void SubmitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nameInput.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated Display Name !");
        string _DISPLAYNAME = nameInput.text;
        usernameWindow.SetActive(false);
        PlayerPrefs.SetString("DISPLAYNAME", _DISPLAYNAME);

        TextMeshProUGUI username = GameObject.Find("displayName").GetComponent<TextMeshProUGUI>();
        username.text = PlayerPrefs.GetString("DISPLAYNAME");

        Debug.Log(result);

    }

    void OnError(PlayFabError error)
    {
        // messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }


}
