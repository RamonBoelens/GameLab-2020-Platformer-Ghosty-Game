using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public CardDisplay cardDisplay;

    public List<diversiChoiceCard> choiceCards;
    public List<diversiGuideCard> guideCards;
    public List<diversiRiskCard> riskCards;
    public List<diversiShareCard> shareCards;
    public List<diversiSmartsCard> smartsCards;

    public List<diversiChoiceCard> markedChoiceCards;
    public List<diversiGuideCard> markedGuideCards;
    public List<diversiRiskCard> markedRiskCards;
    public List<diversiShareCard> markedShareCards;
    public List<diversiSmartsCard> markedSmartsCards;

    private List<int> score;
    private int playerTurn = 0;

    private void Start()
    {
        NextCard();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            MarkCard();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextCard();
        }
    }

    public void AddScore(int playerIndex, int points)
    {
        if (playerIndex < score.Count)
        {
            Debug.LogError("Could not add score because player " + playerIndex + " does not exist!");
            return;
        }

        score[playerIndex] += points;
        Debug.Log("Player " + playerIndex + " total score: " + score[playerIndex]);
    }

    public void MarkCard()
    {
        diversiChoiceCard choiceCard = cardDisplay.GetChoiceCard();
        diversiGuideCard guideCard = cardDisplay.GetGuideCard();
        diversiRiskCard riskCard = cardDisplay.GetRiskCard();
        diversiShareCard shareCard = cardDisplay.GetShareCard();
        diversiSmartsCard smartsCard = cardDisplay.GetSmartsCard();

        if (choiceCard != null)
        {
            // Check if the card isn't already marked
            foreach (diversiChoiceCard markedCard in markedChoiceCards)
            {
                if (markedCard == choiceCard)
                {
                    Debug.LogWarning("The current card " + choiceCard.Name + " is already marked!");
                    return;
                }
            }

            // Else add the card to the marked cards list
            markedChoiceCards.Add(choiceCard);
        }

        else if (guideCard != null)
        {
            // Check if the card isn't already marked
            foreach (diversiGuideCard markedCard in markedGuideCards)
            {
                if (markedCard == guideCard)
                {
                    Debug.LogWarning("The current card " + guideCard.Name + " is already marked!");
                    return;
                }
            }

            // Else add the card to the marked cards list
            markedGuideCards.Add(guideCard);
        }

        else if (riskCard != null)
        {
            // Check if the card isn't already marked
            foreach (diversiRiskCard markedCard in markedRiskCards)
            {
                if (markedCard == riskCard)
                {
                    Debug.LogWarning("The current card " + riskCard.Name + " is already marked!");
                    return;
                }
            }

            // Else add the card to the marked cards list
            markedRiskCards.Add(riskCard);
        }

        else if (shareCard != null)
        {
            // Check if the card isn't already marked
            foreach (diversiShareCard markedCard in markedShareCards)
            {
                if (markedCard == shareCard)
                {
                    Debug.LogWarning("The current card " + shareCard.Name + " is already marked!");
                    return;
                }
            }

            // Else add the card to the marked cards list
            markedShareCards.Add(shareCard);
        }

        else if (smartsCard != null)
        {
            // Check if the card isn't already marked
            foreach (diversiSmartsCard markedCard in markedSmartsCards)
            {
                if (markedCard == smartsCard)
                {
                    Debug.LogWarning("The current card " + smartsCard.Name + " is already marked!");
                    return;
                }
            }

            // Else add the card to the marked cards list
            markedSmartsCards.Add(smartsCard);
        }

        else
        {
            Debug.LogError("Can't mark the card!");
        }
    }

    public void NextCard()
    {
        // Check if there are any cards left
        int choiceCardsCount = choiceCards.Count;
        int guideCardsCount = guideCards.Count;
        int riskCardsCount = riskCards.Count;
        int shareCardsCount = shareCards.Count;
        int smartsCardsCount = smartsCards.Count;

        if (choiceCardsCount <= 0 && guideCardsCount <= 0 && riskCardsCount <= 0 && shareCardsCount <= 0 && smartsCardsCount <= 0)
        {
            // Game Over -> Show go over marked cards


            // TEMP vvvvv
            Debug.LogError("No cards left.");
            return;
        }

        // If there are cards left select one
        int i = Random.Range(1, 6);

        if (i == 1)
        {
            // Check if there are cards left in the pile
            if (choiceCardsCount > 0)
            {
                // Select a random card out of the pile
                int card = Random.Range(0, choiceCardsCount);

                if (choiceCards[card] == null)
                {
                    Debug.LogError("Choice card does not exist!");
                    return;
                }
                else
                {
                    // cardDisplay.UpdateChoiceCard(choiceCards[card]);
                    cardDisplay.UpdateChoiceCard(choiceCards[0]);
                }
            }
        }

        else if (i == 2)
        {
            // Check if there are cards left in the pile
            if (guideCardsCount > 0)
            {
                // Select a random card out of the pile
                int card = Random.Range(0, guideCardsCount);

                if (guideCards[card] == null)
                {
                    Debug.LogError("Guide card does not exist!");
                    return;
                }
                else
                {
                    // cardDisplay.UpdateGuideCard(guideCards[card]);
                    cardDisplay.UpdateGuideCard(guideCards[0]);
                }
            }
        }

        else if (i == 3)
        {
            // Check if there are cards left in the pile
            if (riskCardsCount > 0)
            {
                // Select a random card out of the pile
                int card = Random.Range(0, riskCardsCount);

                if (riskCards[card] == null)
                {
                    Debug.LogError("Risk card does not exist!");
                    return;
                }
                else
                {
                    // cardDisplay.UpdateRiskCard(riskCards[card]);
                    cardDisplay.UpdateRiskCard(riskCards[0]);
                }
            }
        }

        else if (i == 4)
        {
            // Check if there are cards left in the pile
            if (shareCardsCount > 0)
            {
                // Select a random card out of the pile
                int card = Random.Range(0, shareCardsCount);

                if (shareCards[card] == null)
                {
                    Debug.LogError("Share card does not exist!");
                    return;
                }
                else
                {
                    // cardDisplay.UpdateShareCard(shareCards[card]);
                    cardDisplay.UpdateShareCard(shareCards[0]);
                }
            }
        }

        else if (i == 5)
        {
            // Check if there are cards left in the pile
            if (smartsCardsCount > 0)
            {
                // Select a random card out of the pile
                int card = Random.Range(0, smartsCardsCount);

                if (smartsCards[card] == null)
                {
                    Debug.LogError("Smarts card does not exist!");
                    return;
                }
                else
                {
                    // cardDisplay.UpdateSmartsCard(smartsCards[card]);
                }
            }
        }

        else
        {
            Debug.LogError("Can't find the next card!");
        }
    }
}
