using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum gamemode { cards, rollingCube };

public class Gamemanager : MonoBehaviour
{
    [Header("References")]
    public CardDisplay cardDisplay;
    public GameObject team;

    private Scoreboard scoreboard;
    private GameDatabase gameDatabase;
    private CSVScriptReader backupDatabase;

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
    // End Temporary! ----------------------------------------------------------

    private int playerTurn;
    private gamemode currentGamemode;

    #region Setup Game
    private void Awake()
    {
        // References
        gameDatabase = FindObjectOfType<GameDatabase>();
        scoreboard = GetComponent<Scoreboard>();

        // If the database for this game is non-existent .. 
        if (gameDatabase == null)
        {
            // .. then create one!
            CreateDatabase();
        }
    }

    private void CreateDatabase()
    {
        GameObject GO = new GameObject("Game Database");
        GO.AddComponent<GameDatabase>();

        gameDatabase = GO.GetComponent<GameDatabase>();
    }

    private void CreateCardDatabase()
    {
        GameObject GO = new GameObject("Card Database");
        GO.AddComponent<CSVScriptReader>();

        backupDatabase = GO.GetComponent<CSVScriptReader>();
    }

    private void Start()
    {
        SetupPlayers();
        SetupScoreboard();

        // Setup the player highlight
        scoreboard.UpdateTurn(playerTurn);

        // Setup the gamemode
        currentGamemode = gameDatabase.GetGamemode();

        SetupCards();


        // Temporary! ----------------

        NextCard();
        // End Temporary! ------------

        
    }

    private void SetupPlayers()
    {
        // Get the players from the player storage component
        players = team.GetComponent<PlayersStorage>().GetPlayerNames();

        // Check if the players list exists
        if (players == null)
        {
            // Create a list when it doesn't exist
            players = new List<string>();
        }

        // Check for players
        if (players.Count <= 0)
        {
            // If there is no player add an dummy player
            players.Add("Dummy player");
        }

        // Choose starting player
        SelectStartingPlayer();
    }

    private void SelectStartingPlayer()
    {
        if (randomizeStartingPlayer)
            playerTurn = Random.Range(0, team.GetComponent<PlayersStorage>().GetPlayerNames().Count);
        else
            playerTurn = 0;
    }

    private void SetupScoreboard()
    {
        PlayerScores playerScores = team.GetComponent<PlayerScores>();

        // Check if there are players
        if (players == null || players.Count <= 0)
        {
            // If not throw an error and return out of the function
            Debug.LogError("Not enough players to setup the scoreboard!");
            return;
        }

        // Check if the scores were initialized
        if (GetCurrentScores() == null || GetCurrentScores().Length == 0)
        {
            // If it's not then do the setup with the new player list
            playerScores.SetupScores(players.Count);
        }

        scoreboard.SetupScoreboardDisplay(players);
        scoreboard.UpdateScoreDisplay(GetCurrentScores());
    }

    public void SetupCards()
    {
        // If there are no cards in the current game then add some
        if (gameDatabase.GetCards() == null || gameDatabase.GetCards().Count <= 0)
        {
            // If there is no object holding the whole database then .. 
            if (FindObjectOfType<CSVScriptReader>() == null)
            {
                // .. create a new database
                CreateCardDatabase();

                // And add a maximum of 10 dummy cards to the game
                List<Card> backupCards = backupDatabase.GetCards();
                Debug.Log(backupCards.Count);
                int cardsAdded = 10;

                if (backupCards.Count <= 10)
                {
                    cardsAdded = backupCards.Count;
                }

                // Add the cards to the database
                for (int i = 0; i < cardsAdded; i++)
                {
                    gameDatabase.AddCard(backupCards[i]);
                }
            }
        }

        // Depending on the gamemode setup the cards differently
        if (currentGamemode == gamemode.cards)
            SetupCardsOnlyDeck();
        else if (currentGamemode == gamemode.rollingCube)
            SetupRollingCubeDeck();
    }

    public void SetupCardsOnlyDeck()
    {
        Debug.Log("A game with just cards! " + gameDatabase.GetCards().Count + " cards");

        foreach (Card card in gameDatabase.GetCards())
        {
            Debug.Log(card.txt_Front);
        }

        List<Card> shuffledCards = ShuffleCards(gameDatabase.GetCards());

        foreach (Card card in shuffledCards)
        {
            Debug.Log(card.txt_Front);
        }
    }

    public void SetupRollingCubeDeck()
    {
        Debug.Log("A game with the rolling cube!");
    }

    public List<Card> ShuffleCards(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Card tempCard = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = tempCard;
        }

        return cards;
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

    private void NextPlayer()
    {
        if (playerTurn >= players.Count -1)
            playerTurn = 0;
        else
            playerTurn++;

        // Update the background image
        scoreboard.UpdateTurn(playerTurn);
    }

    private int[] GetCurrentScores()
    {
        return team.GetComponent<PlayerScores>().GetScores();
    }
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
        NextPlayer();

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