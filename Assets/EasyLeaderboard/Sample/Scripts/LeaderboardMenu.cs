using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using PlayFab;

public class LeaderboardMenu : MonoBehaviour
{
    private int _entries = 0;
    private bool _canLoadMoreEntries = true;
    private ScrollRect _scrollRect;

    [Header("UI elements")]
    [SerializeField] private Slider _filterSlider;
    [SerializeField] private LeaderboardEntry _entryPrefab;
    [SerializeField] private Transform _leaderboardEntryParent;
    [SerializeField] private Scrollbar _leaderboardScrollbar;

    [Space(10)]
    [Header("Leaderboard parameters")]
    [SerializeField] private int _maxResultsCount;

    private void Start()
    {
        _scrollRect = _leaderboardEntryParent.GetComponentInParent<ScrollRect>();
        GetLeaderboard(Constants.LeaderboardName, true, 0);
    }

    public void GetLeaderboard(string statisticName, bool friendsOnly, int startPosition)
    {
        _scrollRect.vertical = false;
        FacebookAndPlayFabManager.Instance.GetLeaderboard(statisticName, friendsOnly, _maxResultsCount, GetLeaderboardCallback, startPosition);
    }

    public void GetLeaderboardCallback(GetLeaderboardResult result)
    {
        _scrollRect.vertical = true;
        _filterSlider.interactable = true;

        if (result.Leaderboard.Count < _maxResultsCount)
            _canLoadMoreEntries = false;

        foreach (PlayerLeaderboardEntry playerEntry in result.Leaderboard)
        {
            LeaderboardEntry entry = Instantiate(_entryPrefab.gameObject, _leaderboardEntryParent).GetComponent<LeaderboardEntry>();

            int width = 100;
            int height = 100;

            entry.SetUserPosition(playerEntry.Position + 1);
            entry.SetUserScore(playerEntry.StatValue.ToString());

            // Check if the player entry is for the logged-in Facebook user
            if (playerEntry.DisplayName == FacebookAndPlayFabManager.Instance.FacebookUserId)
            {
                entry.SetUserName(FacebookAndPlayFabManager.Instance.FacebookUserName);
                entry.SetUserPictureSprite(FacebookAndPlayFabManager.Instance.FacebookUserPictureSprite);
            }
            // Check if the player entry is for the logged-in email user
            else if (playerEntry.DisplayName == FacebookAndPlayFabManager.Instance._displayName)
            {
                entry.SetUserName(FacebookAndPlayFabManager.Instance._displayName);
                // Set a default picture for email users or load a custom picture if available
                entry.SetUserPictureSprite(null); // Replace with your own default sprite
            }
            else
            {
                // Handle other players (non-logged-in users) as required
            }


            _entries++;
        }
    }


    public void ClearLeaderboard()
    {
        for (int i = 0; i < _leaderboardEntryParent.childCount; i++)
        {
            Destroy(_leaderboardEntryParent.GetChild(i).gameObject);
        }
    }

    public void OnScrollbarValueChanged()
    {
        if (_leaderboardScrollbar.value == 0)
        {
            if (_canLoadMoreEntries)
                GetLeaderboard(Constants.LeaderboardName, _filterSlider.value == 0, _entries);
        }
    }

    public void OnFilterChanged()
    {
        if (!FacebookAndPlayFabManager.Instance.IsLoggedOnPlayFab)
            return;

        ClearLeaderboard();

        _filterSlider.interactable = false;

        GetLeaderboard(Constants.LeaderboardName, _filterSlider.value == 0, 0);
    }

    public void Back()
    {
        SceneManager.LoadScene("newMenu");
    }

    public void Share()
    {
        FacebookAndPlayFabManager.Instance.ShareOnFacebook();
    }
}
