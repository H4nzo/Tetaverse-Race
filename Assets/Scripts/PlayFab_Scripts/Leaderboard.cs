using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class Leaderboard : MonoBehaviour
{
    public Transform rowsParent;
    public GameObject rowPrefab;

    private void Start()
    {
        GetLeaderboardAroundPlayer();
    }

    public void GetLeaderboardAroundPlayer()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "Leaderboard",
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderboardAroundPlayerGet, OnError);
    }

    void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    void OnLeaderboardAroundPlayerGet(GetLeaderboardAroundPlayerResult result)
    {
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            if (item.PlayFabId == FacebookAndPlayFabManager.Instance.loggedInPlayfabId)
            {
                foreach (TextMeshProUGUI t in texts)
                {
                    t.color = new Color(255f / 255f, 165f / 255f, 0f);

                }

            }

            Debug.Log(item.Position + " " + item.DisplayName + " " + item.StatValue);
        }

    }

    public void Back()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("newMenu");
    }






}
