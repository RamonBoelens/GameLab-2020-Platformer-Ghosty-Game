using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CardDisplay cardDisplay;

    public Card currentCard;
    public List<Card> cards;
    public List<Card> markedCards;
    public List<GameObject> screens; // 0 = Setup screen. 1 = Main Game. 3 = Marked Cards.

    private int currentCardIndex;

    // Start is called before the first frame update
    void Start()
    {
        // Get screen with all the cards
        // So the facilitator can choose what cards to play with
        EnableScreen(screens[0]);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        // Fill the list with all the selected cards
        // Shuffle the cards if needed
        ShuffleCards();
        // Setup the first card
        currentCardIndex = 0;
        currentCard = cards[currentCardIndex];
        cardDisplay.card = currentCard;
        // Get the screen up for the main game
        EnableScreen(screens[1]);
    }

    public void SetupMarkedCards()
    {
        // Setup the first marked card
        // Get the screen up for the marked cards
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

        // Add the card to the marked cards list
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

    private void EnableScreen(GameObject _screen)
    {
        // Go over the screen list
        foreach (GameObject screen in screens)
        {
            // Check is the screen provided is the same as the screen in the list
            if (screen != _screen)
            {
                // If not then disable the screen (GameObject)
                screen.SetActive(false);
            }
            else
            {
                // Else enable it because it is the right screen
                screen.SetActive(enabled);
            }
        }
    }
}
