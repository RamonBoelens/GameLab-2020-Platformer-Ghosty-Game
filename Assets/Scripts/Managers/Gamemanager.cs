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
    // End Temporary! ----------------------------------------------------------

    // Combined variables
    private List<string> players;
    private int playerTurn;
    private gamemode currentGamemode;
    private Card currentCard;
    private List<Card> markedCards;

    // Just Cards gameplay variables
    private List<Card> shuffledDeck;

    // Rolling cube gameplay variables
    private List<Card> shuffledChoiceDeck;
    private List<Card> shuffledGuideDeck;
    private List<Card> shuffledRiskDeck;
    private List<Card> shuffledsharedDeck;
    private List<Card> shuffledsmartsDeck;

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
        // Shuffle the cards
        shuffledDeck = new List<Card>();
        shuffledDeck = ShuffleCards(gameDatabase.GetCards());
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
    private void RollTheCube()
    {
        // TODO
    }

    #endregion

    #region Gameplay - Shared
    // Next Card (Need deck)
    public void OnNextCardCallback()
    {
        if (currentGamemode == gamemode.cards)
            NextCard(shuffledDeck);
        else if (currentGamemode == gamemode.rollingCube)
            RollTheCube();
    }

    public void OnMarkCardCallback()
    {
        MarkCard(currentCard);
    }

    private void NextCard (List<Card> cards)
    {
        // Check if there are cards left in the deck
        if (cards.Count <= 0)
        {
            Debug.Log("No cards left!");
            return;
        }

        // Set the top card as current card
        currentCard = cards[0];

        // Pick the next card
        cardDisplay.UpdateCard(currentCard);

        // Play animation  

        // Remove card from the deck
        cards.Remove(currentCard);
    }

    // Check Answer (Need currentCard)

    // Add Score to the current player (Need points and current player)

    // Pop card out of the deck (Need currentCard and deck)

    // Mark the current card (Need currentCard)
    private void MarkCard(Card card)
    {
        // Initialize a new list when this one doesn't exist yet.
        if (markedCards == null)
        {
            markedCards = new List<Card>();
        }

        // Check if the card isn't already marked
        foreach (Card markedCard in markedCards)
        {
            if (markedCard == card)
            {
                Debug.Log("The card " + card.txt_Front + " is already marked!");
                return;
            }
        }

        // Else mark the card
        markedCards.Add(card);
        Debug.Log("Marking the following card: " + card.txt_Front);
    }

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
}