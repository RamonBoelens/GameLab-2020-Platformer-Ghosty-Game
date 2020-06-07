using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CSVScriptReader))]
public class CardDatabase : MonoBehaviour
{
    // Singleton Pattern
    private static CardDatabase _instance;
    public static CardDatabase Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this) { Destroy(gameObject); }
        else { _instance = this; }
    }

    public TextAsset fileToLoad = null;

    private List<Card> allCards = new List<Card>();

    private void Start()
    {
        if (!fileToLoad)
        {
            Debug.LogWarning("No database file to load!");
            return;
        }

        // Read the file
        allCards = GetComponent<CSVScriptReader>().ReadCardDatabase(fileToLoad);
    }

    public List<Card> GetCards()
    {
        return allCards;
    }
}
