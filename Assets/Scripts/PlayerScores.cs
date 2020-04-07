using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScores : MonoBehaviour
{
    private int[] playerScores;

    public void SetupScores(int playerAmount)
    {
        Debug.Log("Setup Scores");

        // Initialize the array
        playerScores = new int[playerAmount];

        // Make sure every score is set to 0
        for (int i = 0; i < playerScores.Length; i++)
        {
            playerScores[i] = 0;
        }
    }

    public void AddScore(int playerIndex, int points)
    {
        // Check if that player exists
        if (playerIndex > playerScores.Length)
        {
            Debug.LogError("Could not add score because player " + playerIndex + " does not exist!");
            return;
        }

        // Add the points to the total score of the player
        playerScores[playerIndex] += points;
    }

    public int[] GetScores()
    {
        return playerScores;
    }
}