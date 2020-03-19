using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersStorage : MonoBehaviour
{
    private List<string> playerNamesList = new List<string>();

    public void SavePlayerNames(List<string> playerNames)
    {
        playerNamesList = playerNames;
    }

    public List<string> GetPlayerNames()
    {
        return playerNamesList;
    }
}
