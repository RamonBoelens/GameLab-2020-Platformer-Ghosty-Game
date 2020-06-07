using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenerateTags : MonoBehaviour
{
    public GameObject prefab_Toggle;
    public GameObject[] toggleParents;

    private CardDatabase cardDatabase;
    private Dictionary<string, Toggle> toggles = new Dictionary<string, Toggle>();
    private List<string> tags = new List<string>();

    private void Start()
    {
        // Find the database
        cardDatabase = FindObjectOfType<CardDatabase>();

        // Check if the database is found
        if (!cardDatabase)
        {
            Debug.LogWarning("Couldn't find the database component!");
            return;
        }

        // Generate a list with all the tags and create the toggles based on that list
        GenerateAllTags(cardDatabase.GetCards());
        CreateToggles(tags);
    }

    private void GenerateAllTags(List<Card> _cards)
    {
        if (_cards.Count <= 0)
        {
            Debug.LogWarning("The list from the card database is empty!");
            return;
        }

        // Create a list with all the different tags
        bool newTag = true;

        foreach (Card card in _cards)
        {
            for (int i = 0; i < tags.Count; i++)
            {
                if (card.culture == tags[i] || card.culture == "-" || card.culture == "")
                    newTag = false;
                else
                    newTag = true;
            }

            if (newTag)
            {
                tags.Add(card.culture);
            }
        }
    }

    private void CreateToggles(List<string> _tags)
    {
        // Safety Checks
        if (tags.Count <= 0)
        {
            Debug.LogWarning("No tags were found!");
            return;
        }
        else if (toggleParents.Length <= 0)
        {
            Debug.LogWarning("No parents for the toggles are set!");
            return;
        }

        // Go over all the places where the tags should be generated
        foreach (GameObject parent in toggleParents)
        {
            // Go over each tag to create a toggle
            foreach (string tag in _tags)
            {
                // Create a new toggle and parent it the the toggle parent object
                GameObject toggle = Instantiate(prefab_Toggle, new Vector3(0, 0, 0), Quaternion.identity);
                toggle.transform.SetParent(parent.transform);

                // Change the label name
                toggle.transform.Find("Label").GetComponentInChildren<TextMeshProUGUI>().text = tag;

                // Make sure the toggle is set to false
                toggle.GetComponent<Toggle>().isOn = false;

                // Add toggle to the dictionary of toggles
                toggles.Add(tag, toggle.GetComponent<Toggle>());
            }
        }
    }

    public Dictionary<string, Toggle> GetToggles()
    {
        return toggles;
    }

    public List<string> GetTags()
    {
        return tags;
    }
}