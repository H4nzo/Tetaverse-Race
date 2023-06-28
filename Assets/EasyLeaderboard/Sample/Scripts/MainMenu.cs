using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Text _nameText;
    private Image _profileImage;
    private Sprite _noProfileSprite;
    private GameObject _postLoginActions;
    private Text _scoreText;

    public const string LEVEL = "newMenu";


    private void Start()
    {
        if (FacebookAndPlayFabManager.Instance.IsLoggedOnFacebook)
        {
            _nameText.text = FacebookAndPlayFabManager.Instance.FacebookUserName;
            _profileImage.sprite = FacebookAndPlayFabManager.Instance.FacebookUserPictureSprite;
            _postLoginActions.SetActive(true);
        }
        // else
        // {
        //     LoginWithFacebook();
        // }
    }

    public void LoginWithFacebook()
    {
        if (string.IsNullOrEmpty(FacebookAndPlayFabManager.Instance.PlayFabTitleId))
        {
            Debug.LogError("PlayFabTitleId is null.");
            return;
        }

        if (FacebookAndPlayFabManager.Instance.IsLoggedOnFacebook)
            return;

        FacebookAndPlayFabManager.Instance.LogOnFacebook(successCallback: res =>
        {
            StartCoroutine(GetUserNameRoutine());
            StartCoroutine(GetUserPictureRoutine());
            StartCoroutine(WaitForPlayFabLogin());
            StartCoroutine(LoadLevel(LEVEL));
        });
    }

    private IEnumerator LoadLevel(string level)
    {
        yield return new WaitUntil(() => !string.IsNullOrEmpty(FacebookAndPlayFabManager.Instance.FacebookUserName));
        SceneManager.LoadScene(level);
    }

    // Shows the player's Facebook name as soon as it's available.
    private IEnumerator GetUserNameRoutine()
    {
        yield return new WaitUntil(() => !string.IsNullOrEmpty(FacebookAndPlayFabManager.Instance.FacebookUserName));

        // _nameText.text = FacebookAndPlayFabManager.Instance.FacebookUserName;
    }

    // Shows the player's Facebook picture as soon as it's available.
    private IEnumerator GetUserPictureRoutine()
    {
        yield return new WaitUntil(() => FacebookAndPlayFabManager.Instance.FacebookUserPictureSprite != null);

        _profileImage.sprite = FacebookAndPlayFabManager.Instance.FacebookUserPictureSprite;
    }

    // Enable a set of buttons as soon as the player gets logged on PlayFab.
    private IEnumerator WaitForPlayFabLogin()
    {
        yield return new WaitUntil(() => FacebookAndPlayFabManager.Instance.IsLoggedOnPlayFab);

        _postLoginActions.SetActive(true);
    }

    public void Logout()
    {
        FacebookAndPlayFabManager.Instance.LogOutFacebook();
        _profileImage.sprite = _noProfileSprite;
        _nameText.text = "My Facebook Name";
        _postLoginActions.SetActive(false);
    }

    public void PostScoreOnPlayFab()
    {
        if (string.IsNullOrEmpty(_scoreText.text))
            return;

        var score = System.Convert.ToInt32(_scoreText.text);

        FacebookAndPlayFabManager.Instance.UpdateStat(Constants.LeaderboardName, score);
    }

    public void SeeLeaderboard()
    {
        SceneManager.LoadScene(1);
    }
}