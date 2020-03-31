using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    [Header("References")]
    public CardDisplay cardDisplay;
    public GameObject team;

    // Temporary! --------------------------------------------------------------
    [Header("Cards in the game")]
    public List<diversiChoiceCard> choiceCards;
    public List<diversiGuideCard> guideCards;
    public List<diversiRiskCard> riskCards;
    public List<diversiShareCard> shareCards;
    public List<diversiSmartsCard> smartsCards;
    // End Temporary! ----------------------------------------------------------

    [Header("Options")]
    public bool randomizeStartingPlayer;

    // Temporary! --------------------------------------------------------------
    private List<diversiChoiceCard> markedChoiceCards = new List<diversiChoiceCard>();
    private List<diversiGuideCard> markedGuideCards = new List<diversiGuideCard>();
    private List<diversiRiskCard> markedRiskCards = new List<diversiRiskCard>();
    private List<diversiShareCard> markedShareCards = new List<diversiShareCard>();
    private List<diversiSmartsCard> markedSmartsCards = new List<diversiSmartsCard>();

    private List<string> players;
    private int[] scores;
    // End Temporary! ----------------------------------------------------------

    private int playerTurn;

    public enum gamemode { cards, rollingCube };
    gamemode currentGamemode;

    #region Setup Game
    private void Start()
    {
        if (randomizeStartingPlayer)
            playerTurn = RandomizeStartingPlayer();
        else
            playerTurn = 0;

        SetupCards();

        // Temporary! ----------------
        Scoreboard scoreboard = GetComponent<Scoreboard>();

        players = team.GetComponent<PlayersStorage>().GetPlayerNames();
        scores = team.GetComponent<PlayerScores>().GetScores();

        scoreboard.SetupScoreboardDisplay(players);
        scoreboard.UpdateScoreDisplay(scores);


        NextCard();
        // End Temporary! ------------
    }

    private int RandomizeStartingPlayer()
    {
        return Random.Range(0, team.GetComponent<PlayersStorage>().GetPlayerNames().Count);
    }

    public void SetupGameMode(gamemode _gamemode)
    {
        currentGamemode = _gamemode;
    }

    public void SetupCards()
    {
        // If the game mode is just cards ..
        if (currentGamemode == gamemode.cards)
        {
            // .. then shuffle all the cards and put them in one deck
            Debug.Log("A game with just cards!");
        }
        // Else if the game mode is with a rolling cube ..
        else if (currentGamemode == gamemode.rollingCube)
        {
            // .. then create 5 decks with the different cards and shuffle them individually
            Debug.Log("A game with the rolling cube!");
        }
        else
        {
            Debug.LogError("Can't find a gamemode!");
        }
    }

    public void ShuffleCards()
    {
        // Take a deck of cards and shuffle them!
    }
    #endregion
    #region Gameplay - Just Cards

    #endregion
    #region Gameplay - Rolling Cube
    // Role the cube to select the next category

    #endregion
    #region Gameplay - Shared
    // Next Card (Need deck)

    // Check Answer (Need currentCard)

    // Add Score to the current player (Need points and current player)

    // Pop card out of the deck (Need currentCard and deck)

    // Mark the current card (Need currentCard)

    // Next turn (Need current players' turn)
    #endregion


    #region Temp code
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

        //cardDisplay.transform.RotateAround(transform.position, transform.up, Time.deltaTime * 45f);
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
    #endregion TempCode
}