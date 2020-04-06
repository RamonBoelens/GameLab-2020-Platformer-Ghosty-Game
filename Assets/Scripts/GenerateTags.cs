using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenerateTags : MonoBehaviour
{
    public GameObject prefab_Toggle;
    public GameObject toggleParent;

    private CSVScriptReader database;

    private Dictionary<string, Toggle> toggles = new Dictionary<string, Toggle>();
    private List<string> tempTags = new List<string>();
    private List<string> tags = new List<string>();

    private void Start()
    {
        // Find the database
        database = FindObjectOfType<CSVScriptReader>();

        GenerateAllTags(database.GetCards());
        CheckAllTags();
    }

    private void GenerateAllTags(List<Card> _cards)
    {
        // Go over all the cards and add their tags to a temporary list
        foreach (Card card in _cards)
        {
            if (card.culture != "" || card.culture != "-")
            {
                AddTag(card.culture);
            }
        }
    }

    private void CheckAllTags()
    {
        for (int i = 0; i < tempTags.Count; i++)
        {
            AddTag(tempTags[i]);
        }
    }

    private void AddTag(string tag)
    {
        // Go over all the added tags
        for (int i = 0; i < tags.Count; i++)
        {
            // If the tag already exist then return out of the function
            if (tag == tags[i])
            {
                return;
            }
        }

        // Add the tag to the list and create a toggle
        tags.Add(tag);
        CreateToggle(tag);
    }

    private void CreateToggle(string tag)
    {
        // Create a new toggle and parent it the the toggle parent object
        GameObject toggle = Instantiate(prefab_Toggle, new Vector3(0,0,0), Quaternion.identity);
        toggle.transform.SetParent(toggleParent.transform);

        // Change the label name
        toggle.transform.Find("Label").GetComponentInParent<TextMeshProUGUI>().text = tag;

        // Make sure the toggle is set to false
        toggle.GetComponent<Toggle>().isOn = false;

        // Add toggle to the dictionary of toggles
        toggles.Add(tag ,toggle.GetComponent<Toggle>());
    }

    public bool ToggleIsTrue (string tag)
    {
        Toggle toggle = null;

        if (toggles.TryGetValue(tag, out toggle))
            return toggle.isOn;
        else
        {
            Debug.Log("Couldn't find the tag!");
            return false;
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