using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndStats : MonoBehaviour
{
    private List<string> playerNames = new List<string>();
    private int[] endScores;

    public void SaveData(List<string> names, int[] scores)
    {
        playerNames = names;
        endScores = scores;
    }

    public List<string> GetPlayers()
    {
        return playerNames;
    }

    public int[] GetScores()
    {
        return endScores;
    }
}
