using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GO_Switcher : MonoBehaviour
{
    public List<GameObject> gameObjects; // All the objects to switch between
    public int startObject;              // First object to enable

    private void Start()
    {
        EnableObject(gameObjects[startObject]);
    }

    public void EnableObject(GameObject gameObject)
    {
        // Go over the list with gameObjects
        foreach (GameObject GO in gameObjects)
        {
            // Check if the gameObject provided is the same as the gameObject in the list
            if (GO != gameObject)
            {
                // If not then disable the gameObject
                GO.SetActive(false);
            }
            else
            {
                // Else enable the gameObject
                GO.SetActive(true);
            }
        }
    }
}
