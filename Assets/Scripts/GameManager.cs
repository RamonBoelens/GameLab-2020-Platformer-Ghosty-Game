using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CardDisplay cardDisplay;

    public Card currentCard;
    public List<Card> cards;
    public List<Card> markedCards;

    private int currentCardIndex;

    // Start is called before the first frame update
    void Start()
    {
        ShuffleCards();

        currentCardIndex = 0;
        currentCard = cards[currentCardIndex];

        cardDisplay.card = currentCard;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextCard();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            MarkCard();
        }
    }

    public void MarkCard()
    {
        foreach (Card card in markedCards)
        {
            if (card == currentCard)
            {
                Debug.LogWarning("The current card "+ currentCard.name + " is already marked!");
                return;
            }
        }

        markedCards.Add(currentCard);
    }

    public void NextCard()
    {
        currentCardIndex++;

        if (currentCardIndex > cards.Count -1)
        {
            currentCardIndex = 0;
        }

        currentCard = cards[currentCardIndex];

        cardDisplay.card = currentCard;
        cardDisplay.UpdateCard();
    }

    private void ShuffleCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Card temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }
}
