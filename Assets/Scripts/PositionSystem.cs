using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerPositionInfo
{
    public string playerName;
    public int position;
}

public class PositionSystem : MonoBehaviour
{
    public Dictionary<string, PlayerPositionInfo> playerPositions = new Dictionary<string, PlayerPositionInfo>();

    [HideInInspector] public int position_1 = 1;
    [HideInInspector] public int position_2 = 2;
    [HideInInspector] public int position_3 = 3;

    private HashSet<int> occupiedPositions = new HashSet<int>();

    public GameObject scoreRowPrefab;
    public Transform rowParentPrefab;

    [HideInInspector] public TextMeshPro[] texts;

    public int GetNextAvailablePosition()
    {
        for (int position = position_3; position >= position_1; position--)
        {
            if (!occupiedPositions.Contains(position))
            {
                return position;
            }
        }

        return -1; // No available position
    }

    public void AssignPlayerPosition(string playerName, int desiredPosition)
    {
        int position = GetNextAvailablePosition();

        if (position == -1)
        {
            Debug.LogWarning("No available position for player: " + playerName);
            return; // Early exit if no position is available
        }

        if (occupiedPositions.Contains(desiredPosition))
        {
            Debug.LogWarning("The desired position " + desiredPosition + " is already occupied.");
            return; // Early exit if desired position is occupied
        }

        PlayerPositionInfo playerPositionInfo = new PlayerPositionInfo
        {
            playerName = playerName,
            position = desiredPosition
        };

        playerPositions[playerName] = playerPositionInfo;
        occupiedPositions.Add(desiredPosition);
        Debug.Log(desiredPosition + " mapped to " + playerName);
    }

    public void DisplayScoreboard()
    {
        foreach (var kvp in playerPositions)
        {
            GameObject newGO = Instantiate(scoreRowPrefab, rowParentPrefab);

            // Get the TextMeshPro components from the instantiated row
            TextMeshPro[] texts = newGO.GetComponentsInChildren<TextMeshPro>();

            if (texts.Length >= 2)
            {
                // Update the TextMeshPro components with player name and position
                texts[0].text = kvp.Value.playerName;   // Set the player's name
                texts[1].text = kvp.Value.position.ToString();  // Set the player's position
            }
            else
            {
                Debug.LogError("Not enough TextMeshPro components found in the score row prefab.");
            }
        }
    }
}
