using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/3D/DiversiSMARTS Card")]
public class diversiSmartsCard : ScriptableObject
{
    [Header("Basic Information.")]
    [SerializeField] private new string name;
    [SerializeField] private Material cardFront;
    [SerializeField] private Material cardBack;

    [Header("Front of the card.")]
    [SerializeField] private Sprite artwork;
    [TextArea(3, 10)] [SerializeField] private string instruction;

    [Header("Back of the card.")]
    [TextArea(3, 10)] [SerializeField] private string bck_Answer;
    [TextArea(3, 10)] [SerializeField] private string bck_FollowUp;

    [Header("Answer.")]
    [SerializeField] private string[] answers;
    [Space(5)]
    [Tooltip("Fill in the corresponding element number from the answers array. \nIf element 0 is the correct answer fill in 0.\nIf element 1 is the correct answer fill in 1.\nETC.")]
    [SerializeField] private int correctAnswer;

    private int points = 3;

    public int Points => points;
    public string Name => name;
    public Material CardFront => cardFront;
    public Material CardBack => cardBack;
    public string Instruction => instruction;
    public string BCK_Answer => bck_Answer;
    public string BCK_FollowUp => bck_FollowUp;
    public string[] Answers => answers;
    public int CorrectAnswer => correctAnswer;
    public Sprite Artwork => artwork;
}
