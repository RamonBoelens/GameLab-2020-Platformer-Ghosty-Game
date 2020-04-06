using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDatabase : MonoBehaviour
{
    List<Card> gameCards = new List<Card>();
    gamemode gamemode;
    public void AddCard(Card card)
    {
        for (int i = 0; i < gameCards.Count; i++)
        {
            if (card == gameCards[i])
            {
                return;
            }
        }

        gameCards.Add(card);
    }

    public void SetGamemode(string dropdownValue)
    {
        if (dropdownValue == "Only Cards")
        {
            gamemode = gamemode.cards;
        }
        else if (dropdownValue == "Rolling Cube")
        {
            gamemode = gamemode.rollingCube;
        }
        else
        {
            Debug.LogWarning("Can't set the gamemode to " + dropdownValue + " because that doens't exist!");
        }
    }

    public List<Card> GetCards()
    {
        return gameCards;
    }

    public gamemode GetGamemode()
    {
        return gamemode;
    }
}
