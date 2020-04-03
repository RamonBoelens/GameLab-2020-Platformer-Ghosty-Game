using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateGame : MonoBehaviour
{
    public Button btn_CreateGame;

    private GenerateTags generateTags;
    private Dictionary<string, Toggle> toggles = new Dictionary<string, Toggle>();
    private List<string> tags = new List<string>();

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

    private void SetButtonInteractable(bool interactable)
    {
        btn_CreateGame.interactable = interactable;
    }
}
