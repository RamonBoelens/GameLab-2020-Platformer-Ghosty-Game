using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/3D/DiversiRISK Card")]
public class diversiRiskCard : ScriptableObject
{
    [Header("Basic Information.")]
    [SerializeField] private new string name;
    [SerializeField] private Material cardFront;
    [SerializeField] private Material cardBack;

    [Header("Front of the card.")]
    [SerializeField] private Sprite artwork;
    [TextArea(3, 10)] [SerializeField] private string instruction;
    [TextArea(3, 10)] [SerializeField] private string followUp;

    private int points = 4;


    public int Points => points;
    public string Name => name;
    public Material CardFront => cardFront;
    public Material CardBack => cardBack;
    public string Instruction => instruction;
    public string FollowUp => followUp;
    public Sprite Artwork => artwork;
}
