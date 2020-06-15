using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndStats : MonoBehaviour
{
    private List<string> playerNames = new List<string>();
    private int[] endScores;
    private List<Card> markedCards = new List<Card>();

    public void SaveData(List<string> names, int[] scores, List<Card> cards)
    {
        playerNames = names;
        endScores = scores;
        markedCards = cards;
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
