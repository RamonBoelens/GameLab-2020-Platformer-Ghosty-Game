using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDatabase : MonoBehaviour
{
    List<Card> gameCards = new List<Card>();

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

        Debug.Log("Added Card to the game! " + card.txt_Front);
    }
}
