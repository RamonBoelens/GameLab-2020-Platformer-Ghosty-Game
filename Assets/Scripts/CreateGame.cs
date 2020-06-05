using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateGame : MonoBehaviour
{
    public Button btn_CreateGame;
    public TMP_Dropdown dropdown;

    private GenerateTags generateTags;
    private Dictionary<string, Toggle> toggles = new Dictionary<string, Toggle>();
    private List<string> tags = new List<string>();

    private List<string> addedTags = new List<string>();

    private void Start()
    {
        btn_CreateGame.interactable = false;

        generateTags = GetComponent<GenerateTags>();
    }

    public void CheckToggles()
    {
        toggles = generateTags.GetToggles();
        tags = generateTags.GetTags();

        Toggle toggle = null;

        // Go over all the toggles
        for (int i = 0; i < tags.Count; i++)
        {
            if (toggles.TryGetValue(tags[i], out toggle))
            {
                // If the toggle is true ..
                if (toggle.isOn)
                {
                    // .. then set the create game button to interactable and quit this function
                    SetButtonInteractable(true);
                    return;
                }
            }
            else
            {
                Debug.LogError("Tag " + tags[i] + " does not exist!");
            }
        }

        // If none of the toggles are true then set the create game button to non-interactable
        SetButtonInteractable(false);
    }

    public void Tag(bool isAdding, string tag)
    {
        if (isAdding)
            addedTags.Add(tag);
        else if (!isAdding)
            addedTags.Remove(tag);
    }

    private void SetButtonInteractable(bool interactable)
    {
        btn_CreateGame.interactable = interactable;
    }

    public void SetupGame()
    {
        // Get a reference to the card database
        CSVScriptReader database = FindObjectOfType<CSVScriptReader>();
        List<Card> cardDatabase = database.GetCards();

        // Get a reference to the GameDatabase
        GameSettings gameDatabase = FindObjectOfType<GameSettings>();

        // Go over each card in the database
        for (int i = 0; i < cardDatabase.Count; i++)
        {
            // Go over each chosen tag
            for (int j = 0; j < addedTags.Count; j++)
            {
                // If the tag is the same as the one on the card in the database ..
                if (cardDatabase[i].culture == addedTags[j])
                {
                    // .. Then add the card to the gamedatabase
                    gameDatabase.AddCard(cardDatabase[i]);
                }
            }
        }

        // Set the gamemode
        //gameDatabase.SetGamemode(dropdown.options[dropdown.value].text);
    }
}
