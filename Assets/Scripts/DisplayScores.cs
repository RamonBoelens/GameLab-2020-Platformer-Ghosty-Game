using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScores : MonoBehaviour
{
    EndStats endstats;
    GameObject parent;

    private void Start()
    {
        endstats = FindObjectOfType<EndStats>();

        DisplayEndResult();

        FindParent();
    }

    private void DisplayEndResult()
    {
        if (parent == null)
            FindParent();

        List<string> players = endstats.GetPlayers();
        int[] scores = endstats.GetScores();

        for (int i = 0; i < players.Count; i++)
        {
            GameObject GO = new GameObject("Player Result");
            GO.AddComponent<TextMeshProUGUI>();
            GO.transform.SetParent(parent.transform, false);

            RectTransform rect = GO.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(8.6f, 0.5f);

            TextMeshProUGUI textfield = GO.GetComponent<TextMeshProUGUI>();
            textfield.text = players[i] + " had a total score of " + scores[i] + "!";
            textfield.enableWordWrapping = false;
            textfield.alignment = TextAlignmentOptions.Left;
            textfield.fontSize = 0.43f;
        }
    }

    private void FindParent()
    {
        parent = GameObject.Find("EndStats Parent");
    }
}
