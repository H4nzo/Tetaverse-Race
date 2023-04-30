using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    [Header("Windows")]
    public GameObject nameWindow;

    [Header("Display Name Window")]
    public GameObject nameError;
    public InputField nameInput;

    [Header("UI")]
    [SerializeField] InputField emailInput;
    [SerializeField] InputField passwordInput;
    [SerializeField] Text messageText;

    public GameObject MainWindow, loginPanel;

    private static PlayfabManager instance;

    // Use Awake() to make this script an instance and prevent it from being destroyed when a new scene is loaded
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        string _email = PlayerPrefs.GetString("VALID_EMAIL").ToString();
        string _password = PlayerPrefs.GetString("VALID_PASSWORD").ToString();

        if (_email != null && _password != null)
        {
            var request = new LoginWithEmailAddressRequest
            {
                Email = _email,
                Password = _password,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetPlayerProfile = true
                }
            };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
        }
        else
        {
            return;
        }

        // Login();
    }

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
        messageText.text = $"Register and Login Successful";
        string _EMAIL = emailInput.text;
        string _PASSWORD = passwordInput.text;
        PlayerPrefs.SetString("VALID_EMAIL", _EMAIL);
        PlayerPrefs.SetString("VALID_PASSWORD", _PASSWORD);
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

        if (result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
            PlayerPrefs.SetString("DISPLAYNAME", name);
            loginPanel.SetActive(false);
            MainWindow.SetActive(true);
            Text username = GameObject.Find("DisplayNameText").GetComponent<Text>();
            username.text = PlayerPrefs.GetString("DISPLAYNAME");
        }
        if (name == null)
            nameWindow.SetActive(true);

        string _EMAIL = emailInput.text;
        string _PASSWORD = passwordInput.text;
        PlayerPrefs.SetString("VALID_EMAIL", _EMAIL);
        PlayerPrefs.SetString("VALID_PASSWORD", _PASSWORD);
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
        PlayerPrefs.SetString("DISPLAYNAME", _DISPLAYNAME);
    }

    void OnError(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

   






}
