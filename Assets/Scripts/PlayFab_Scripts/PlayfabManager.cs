using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class PlayfabManager : MonoBehaviour
{
    [Header("Windows")]
    public GameObject nameWindow;
    public GameObject loginScreen;

    [Header("Display Name Window")]
    public GameObject nameError;
    public InputField nameInput;

    [Header("UI")]
    [SerializeField] InputField emailInput;
    [SerializeField] InputField passwordInput;
    [SerializeField] Text messageText;



    public GameObject MainWindow, loginPanel;

    private static PlayfabManager instance;

    public bool isLoggedIn;


    // Use Awake() to make this script an instance and prevent it from being destroyed when a new scene is loaded
     void Awake()
    {
        //Singleton method
        
        if (instance == null)
        {
            //First run, set the instance
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (instance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        
    //    AutoLogin();
     
        if (PlayerPrefs.HasKey("VALID_EMAIL"))
        {
           string userEmail = PlayerPrefs.GetString("VALID_EMAIL");
           string userPassword = PlayerPrefs.GetString("VALID_PASSWORD");

            var request = new LoginWithEmailAddressRequest{Email = userEmail, Password = userPassword, InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }};
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);

        }



    }

    // private void Start()
    // {
    //     AutoLogin();
    // }

    public void RegisterButton()
    {
        if (passwordInput.text.Length < 6)
        {
            messageText.text = $"Password too short!";
            return;
        }

        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        isLoggedIn = true;
        messageText.text = $"Register and Login Successful";
        string _EMAIL = emailInput.text;
        string _PASSWORD = passwordInput.text;
        PlayerPrefs.SetString("VALID_EMAIL", _EMAIL);
        PlayerPrefs.SetString("VALID_PASSWORD", _PASSWORD);
        //  loginPanel.SetActive(false);
        loginScreen.SetActive(false);
        nameWindow.SetActive(true);
    }


    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }


    public void ResetButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "B93DC"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        messageText.text = "Password reset mail sent!";
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Logged in!";
        Debug.Log($"Successful login/account creation!");
        string name = null;
        isLoggedIn = true;

        if (result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
            PlayerPrefs.SetString("DISPLAYNAME", name);
            // loginPanel.SetActive(false);
            loginScreen.SetActive(false);
            MainWindow.SetActive(true);
            Text username = GameObject.Find("DisplayNameText").GetComponent<Text>();
            username.text = PlayerPrefs.GetString("DISPLAYNAME");
        }
        else if (name == null)
        {
            nameWindow.SetActive(true);
        }


        string _EMAIL = emailInput.text;
        string _PASSWORD = passwordInput.text;
        PlayerPrefs.SetString("VALID_EMAIL", _EMAIL);
        PlayerPrefs.SetString("VALID_PASSWORD", _PASSWORD);
    }

    public void AutoLogin()
    {
        if (!isLoggedIn)
        {
            string email = PlayerPrefs.GetString("VALID_EMAIL");
            string password = PlayerPrefs.GetString("VALID_PASSWORD");
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                var request = new LoginWithEmailAddressRequest
                {
                    Email = email,
                    Password = password,
                    InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                    {
                        GetPlayerProfile = true
                    }
                };
                PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
            }
        }
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
        nameWindow.SetActive(false);
        MainWindow.SetActive(true);
        PlayerPrefs.SetString("DISPLAYNAME", _DISPLAYNAME);

        Text username = GameObject.Find("DisplayNameText").GetComponent<Text>();
        username.text = PlayerPrefs.GetString("DISPLAYNAME");

    }

    void OnError(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }









}
