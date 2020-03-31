﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [Header("References")]
    public GameObject prefab_PlayerUI;
    public GameObject ui_PlayerParent;
    public TextMeshProUGUI text_Name;
    public TextMeshProUGUI text_Score;

    private Gamemanager gamemanager;
    private List<TextMeshProUGUI> textField_PlayerScores = new List<TextMeshProUGUI>();

    private void Awake()
    {
        gamemanager = GetComponent<Gamemanager>();
    }

    public void SetupScoreboardDisplay(List<string> names)
    {
        for (int i = 0; i < names.Count; i++)
        {
            // Create new player UI and parent it
            GameObject playerPrefab = Instantiate(prefab_PlayerUI, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            playerPrefab.transform.SetParent(ui_PlayerParent.transform);

            // Change the name
            playerPrefab.transform.Find("TXT_Name").GetComponentInParent<TextMeshProUGUI>().text = names[i];

            // Find and store the player score field
            Transform scoreField = playerPrefab.transform.Find("TXT_Points");
            textField_PlayerScores.Add(scoreField.GetComponentInParent<TextMeshProUGUI>());
        }
    }

    public void UpdateScoreDisplay(int[] scores)
    {
        // Loop through all the scores and update the text on the screen
        for (int i = 0; i < scores.Length; i++)
        {
            textField_PlayerScores[i].GetComponent<TextMeshProUGUI>().text = scores[i].ToString();
        }
    }
}
