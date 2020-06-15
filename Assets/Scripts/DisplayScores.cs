using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayScores : MonoBehaviour
{
    public TMP_FontAsset font;
    public Sprite highlight;

    EndStats endstats;
    GameObject parent;

    private List<int> highestScore = new List<int>();

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

        for (int i = 0; i < scores.Length; i++)
        {
            if (highestScore.Count == 0)
            {
                highestScore.Add(scores[i]);
            }
            else if (scores[i] == highestScore[0])
            {
                highestScore.Add(scores[i]);
            }
            else if (scores[i] > highestScore[0])
            {
                highestScore.Clear();
                highestScore.Add(scores[i]);
            }
        }

        for (int i = 0; i < players.Count; i++)
        {
            // Create the gameobject with two childs -> GFX and Text Object
            GameObject parentObject = new GameObject("Player Result");
            parentObject.transform.SetParent(parent.transform, false);

            GameObject GFXObject = new GameObject("GFX");
            GFXObject.transform.SetParent(parentObject.transform, false);

            GameObject textObject = new GameObject("TXT_Score");
            textObject.transform.SetParent(parentObject.transform, false);

            // Setup parent object
            parentObject.AddComponent<RectTransform>();

            // Setup the graphics object
            GFXObject.AddComponent<Image>();
            GFXObject.GetComponent<Image>().sprite = highlight;
            GFXObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.68f);

            // Disable the sprite by default
            GFXObject.GetComponent<Image>().enabled = false;

            // Setup the text object
            textObject.AddComponent<TextMeshProUGUI>();

            TextMeshProUGUI textfield = textObject.GetComponent<TextMeshProUGUI>();
            textfield.enableWordWrapping = false;
            textfield.alignment = TextAlignmentOptions.Left;
            textfield.fontSize = 0.43f;
            textfield.color = Color.black;
            textfield.font = font;

            // Resize the components to their appropriate size
            RectTransform parentRect = parentObject.GetComponent<RectTransform>();
            parentRect.sizeDelta = new Vector2(8.6f, 0.5f);

            RectTransform GFXRect = GFXObject.GetComponent<RectTransform>();
            GFXRect.sizeDelta = new Vector2(8.6f, 0.5f);

            RectTransform textRect = textObject.GetComponent<RectTransform>();
            textRect.sizeDelta = new Vector2(8.6f, 0.5f);

            // Fill in the information in the components
            textfield.text = players[i] + " had a total score of " + scores[i] + "!";

            // Enable the image component if neccesary
            for (int j = 0; j < highestScore.Count; j++)
            {
                if (highestScore[j] == scores[i])
                {
                    GFXObject.GetComponent<Image>().enabled = true;
                }
            }

            /*
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
            textfield.color = Color.black;
            textfield.font = font;

            for (int j = 0; j < highestScoringPlayerIndex.Count; j++)
            {
                if (highestScoringPlayerIndex[j] == i)
                {
                    GO.AddComponent<Image>();
                    GO.GetComponent<Image>().sprite = highlight;
                }
            }
            */

            
        }
    }

    private void FindParent()
    {
        parent = GameObject.Find("EndStats Parent");
    }
}
