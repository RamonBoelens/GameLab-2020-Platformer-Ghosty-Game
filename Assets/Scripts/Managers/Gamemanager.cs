﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum gamemode { cards, rollingCube };

public class Gamemanager : MonoBehaviour
{
    [Header("References")]
    public CardDisplay cardDisplay;
    public GameObject team;
    public List<GameObject> answerButtons = new List<GameObject>();
    public GameObject answerPanel;
    public Animator cardAnimator;
    public GameObject markedCardsTitle;

    private Scoreboard scoreboard;
    private GameSettings gameDatabase;
    private CSVScriptReader backupDatabase;
    private _SceneManager sceneManager;
    private EndStats endStats;

    [Header("Options")]
    public bool randomizeStartingPlayer;
    public int EndScene;

    // Combined variables
    private List<string> players;
    private int playerTurn;
    private gamemode currentGamemode;
    private Card currentCard;
    private List<Card> markedCards;
    private PlayerScores playerScores;
    private bool cardAnswered;

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
        gameDatabase = FindObjectOfType<GameSettings>();
        scoreboard = GetComponent<Scoreboard>();
        sceneManager = FindObjectOfType<_SceneManager>();

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
        GO.AddComponent<GameSettings>();

        gameDatabase = GO.GetComponent<GameSettings>();
    }

    private void CreateCardDatabase()
    {
        GameObject GO = new GameObject("Card Database");
        GO.AddComponent<CSVScriptReader>();

        backupDatabase = GO.GetComponent<CSVScriptReader>();
    }

    private void CreateSceneManager()
    {
        GameObject GO = new GameObject("SceneManager");
        GO.AddComponent<_SceneManager>();
        GO.AddComponent<DDOL>();

        sceneManager = GO.GetComponent<_SceneManager>();
    }

    private void Start()
    {
        team = GameObject.Find("Team");
        playerScores = team.GetComponent<PlayerScores>();

        SetupPlayers();
        SetupScoreboard();

        // Setup the player highlight
        scoreboard.UpdateTurn(playerTurn);

        // Setup the gamemode
        currentGamemode = gameDatabase.GetGamemode();

        SetupCards();
        SetupFirstCard();
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

        playerScores.SetupScores(players.Count);

        scoreboard.SetupScoreboardDisplay(players);
        scoreboard.UpdateScoreDisplay(GetCurrentScores());
    }

    public void SetupCards()
    {
        // If there are no cards in the current game then add some
        if (gameDatabase.GetSingleGameCardDatabase() == null || gameDatabase.GetSingleGameCardDatabase().Count <= 0)
        {
            // If there is no object holding the whole database then .. 
            if (FindObjectOfType<CSVScriptReader>() == null)
            {
                // .. create a new database
                CreateCardDatabase();
            }

            // And add a maximum of 10 dummy cards to the game
            List<Card> backupCards = FindObjectOfType<CSVScriptReader>().GetCards();

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
        shuffledDeck = ShuffleCards(gameDatabase.GetSingleGameCardDatabase());
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

    private void SetupFirstCard()
    {
        if (currentGamemode == gamemode.cards)
        {
            // Setup the first card
            NextCard(shuffledDeck);
        }
        else if (currentGamemode == gamemode.rollingCube)
        {
            // Let the player roll with the dice etc.
        }
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
        if (cardAnswered == false)
        {
            Debug.Log("Need to answer the card first");
            return;
        }

        // Check last card and add score if needed
        if (currentCard.cardType == CardTypes.Guide || currentCard.cardType == CardTypes.Risk || currentCard.cardType == CardTypes.Share)
        {
            ScorePoints();
        }

        if (currentGamemode == gamemode.cards)
            NextCard(shuffledDeck);
        else if (currentGamemode == gamemode.rollingCube)
            RollTheCube();

        NextPlayer();
    }

    public void OnMarkCardCallback()
    {
        MarkCard(currentCard);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Buzzer") OnNextCardCallback();
                else if (hit.transform.name == "StickyNotes") OnMarkCardCallback();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndGame();
        }
    }

    private void NextCard (List<Card> cards)
    {
        StartCoroutine(RotateCard(true));

        cardAnswered = true;
        answerPanel.SetActive(false);

        // Check if there are cards left in the deck
        if (cards.Count <= 0)
        {
            markedCardsTitle.SetActive(true);

            if (markedCards == null || markedCards.Count == 0)
            {
                EndGame();
            }
            else
            {
                NextMarkedCard(markedCards);
            }
            return;
        }

        // Set the top card as current card
        currentCard = cards[0];

        // Pick the next card
        cardDisplay.UpdateCard(currentCard);

        // Play animation  

        // Remove card from the deck
        cards.Remove(currentCard);

        // Check if the answers need to pop up
        if (currentCard.cardType == CardTypes.Choice || currentCard.cardType == CardTypes.Smarts)
        {
            SetupAnswers();
            cardAnswered = false;
        }
    }

    private void NextMarkedCard (List<Card> cards)
    {
        currentCard = cards[0];

        cardDisplay.UpdateCard(currentCard);

        cards.Remove(currentCard);
    }

    IEnumerator RotateCard(bool RotateBack)
    {
        if (RotateBack == true)
        {
            cardAnimator.SetBool("ani_HasAnswered", false);
            cardAnimator.SetBool("ani_RotateBack", true);
        }
        else
        {
            cardAnimator.SetBool("ani_HasAnswered", true);
            cardAnimator.SetBool("ani_RotateBack", false);
        }

        yield return new WaitForSeconds(1.0f);

        cardAnimator.SetBool("ani_HasAnswered", false);
        cardAnimator.SetBool("ani_RotateBack", false);
    }

    private void SetupAnswers()
    {
        answerPanel.SetActive(true);

        for (int i = 0; i < answerButtons.Count; i++)
        {
            answerButtons[i].SetActive(true);
        }

        if (currentCard.txt_AnswerA == "True or False?")
        {
            answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "True";
            answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "False";

            answerButtons[2].SetActive(false);
            answerButtons[3].SetActive(false);
            answerButtons[4].SetActive(false);

            return;
        }

        if (currentCard.txt_AnswerA != "-" || currentCard.txt_AnswerA != "")
        {
            answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "A";

            if (currentCard.txt_AnswerB != "-" || currentCard.txt_AnswerB != "")
            {
                answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "B";

                if (currentCard.txt_AnswerC != "-" || currentCard.txt_AnswerC != "")
                {
                    answerButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = "C";

                    answerButtons[3].SetActive(false);
                    answerButtons[4].SetActive(false);
                }
                else
                {
                    answerButtons[2].SetActive(false);
                    answerButtons[3].SetActive(false);
                    answerButtons[4].SetActive(false);
                }
            }
            else
            {
                answerButtons[1].SetActive(false);
                answerButtons[2].SetActive(false);
                answerButtons[3].SetActive(false);
                answerButtons[4].SetActive(false);
            }

        }
        else
        {
            answerButtons[0].SetActive(false);
            answerButtons[1].SetActive(false);
            answerButtons[2].SetActive(false);
            answerButtons[3].SetActive(false);
            answerButtons[4].SetActive(false);
        }

    }

    // Check Answer
    public void CheckAnswer(int buttonID)
    {
        StartCoroutine(RotateCard(false));

        if (cardAnswered == true)
        {
            Debug.Log("Already answered!");
            return;
        }

        cardAnswered = true;

        if (currentCard.CorrectAnswer == "TRUE" || currentCard.CorrectAnswer == "A")
        {
            if (buttonID == 0)
            {
                ScorePoints();
                return;
            }
        }

        if (currentCard.CorrectAnswer == "FALSE" || currentCard.CorrectAnswer == "B")
        {
            if (buttonID == 1)
            {
                ScorePoints();
                return;
            }
        }

        if (currentCard.CorrectAnswer == "C")
        {
            if (buttonID == 2)
            {
                ScorePoints();
                return;
            }
        }

        Debug.Log("Wrong answer!");
    }

    // Add Score to the current player (Need points and current player)
    private void ScorePoints()
    {
        playerScores.AddScore(playerTurn, currentCard.pointsValue);
        scoreboard.UpdateScoreDisplay(GetCurrentScores());
    }

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

    private void EndGame()
    {
        endStats = FindObjectOfType<EndStats>();

        // Create an endstats object if it doens't exist
        if (FindObjectOfType<EndStats>() == null)
        {
            GameObject GO = new GameObject("End Stats");
            GO.AddComponent<EndStats>();
            GO.AddComponent<DDOL>();

            endStats = GO.GetComponent<EndStats>();
        }

        // Save players and score
        endStats.SaveData(players, GetCurrentScores(), markedCards);

        if (FindObjectOfType<_SceneManager>() == null)
        {
            CreateSceneManager();
        }

        sceneManager.LoadScene(EndScene);
    }

    private int[] GetCurrentScores()
    {
        return playerScores.GetScores();
    }
    #endregion
}