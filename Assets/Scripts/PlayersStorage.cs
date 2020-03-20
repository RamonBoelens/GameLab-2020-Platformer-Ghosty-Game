using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersStorage : MonoBehaviour
{
    private List<string> playerNamesList;

    public void SavePlayerNames(List<string> playerNames)
    {
        // Initialze the list
        playerNamesList = new List<string>();

        // Create a shallow copy of the list
        for (int i = 0; i < playerNames.Count; i++)
        { 
            playerNamesList.Add(playerNames[i]);
        }
    }

    public List<string> GetPlayerNames()
    {
        return playerNamesList;
    }
}
