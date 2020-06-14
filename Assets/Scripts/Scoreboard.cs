using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [Header("References")]
    public GameObject prefab_PlayerUI;
    public GameObject ui_PlayerParent;
    public TextMeshProUGUI ui_TeamName;

    private Gamemanager gamemanager;
    private List<TextMeshProUGUI> textField_PlayerScores = new List<TextMeshProUGUI>();

    private void Awake()
    {
        gamemanager = GetComponent<Gamemanager>();
    }

    public void SetupScoreboardDisplay(List<string> names)
    { 
        ui_TeamName.text = gamemanager.team.GetComponent<PlayersStorage>().GetTeamName();

        for (int i = 0; i < names.Count; i++)
        {
            // Create new player UI object and parent it
            GameObject playerPrefab = Instantiate(prefab_PlayerUI, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            playerPrefab.transform.SetParent(ui_PlayerParent.transform, false);

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

    public void UpdateTurn(int playerTurnID)
    {
        for (int i = 0; i < textField_PlayerScores.Count; i++)
        {
            textField_PlayerScores[i].transform.parent.Find("Highlight GFX").GetComponent<Image>().enabled = false;
        }

        textField_PlayerScores[playerTurnID].transform.parent.Find("Highlight GFX").GetComponent<Image>().enabled = true;
    }
}