using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScores : MonoBehaviour
{
    EndStats endstats;

    private void Start()
    {
        endstats = FindObjectOfType<EndStats>();

        DisplayEndResult();
    }

    private void DisplayEndResult()
    {
        List<string> players = endstats.GetPlayers();
        int[] scores = endstats.GetScores();

        for (int i = 0; i < players.Count; i++)
        {
            GameObject GO = new GameObject("Player Result");
            GO.AddComponent<TextMeshProUGUI>();
            GO.transform.SetParent(transform);

            TextMeshProUGUI textfield = GO.GetComponent<TextMeshProUGUI>();
            textfield.text = players[i] + " had a total score of " + scores[i] + "!";
            textfield.enableWordWrapping = false;
            textfield.alignment = TextAlignmentOptions.Center;
        }
    }
}
