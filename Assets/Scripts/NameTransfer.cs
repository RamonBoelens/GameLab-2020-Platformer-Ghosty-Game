using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameTransfer : MonoBehaviour
{
    private List<string> players = new List<string>();
    private string theName;

    public Text inputField;
    public Text textDisplay;

    private void Start()
    {
        UpdatePlayerDisplay();
    }

    public void StoreName()
    {
        // Check if the field is empty
            // If empty -> return;

        // Check if the first or last character is a space
            // If so delete those

        // Check if the name already is in the list
            // If so -> return

        // Store the name in a list
        theName = inputField.text;
        players.Add(theName);

        // Update the list of players on the screen
        UpdatePlayerDisplay();
    }

    public void UpdatePlayerDisplay()
    {
        // If there are no players yet then empty the list
        if (players.Count <= 0)
        {
            textDisplay.text = null;
            return;
        }

        textDisplay.text = theName;
    }
}
