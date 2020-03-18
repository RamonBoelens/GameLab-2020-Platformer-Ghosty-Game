using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/3D/DiversiGUIDE Card")]
public class diversiGuideCard : ScriptableObject
{
    [Header("Basic Information.")]
    [SerializeField] private new string name;
    [SerializeField] private Material cardFront;
    [SerializeField] private Material cardBack;

    [Header("Front of the card.")]
    [SerializeField] private Sprite artwork;
    [TextArea(3, 10)] [SerializeField] private string instruction;
    [TextArea(3, 10)] [SerializeField] private string followUp;
    [TextArea(3, 10)] [SerializeField] private string quote;

    private int points = 1;


    public int Points => points;
    public string Name => name;
    public Material CardFront => cardFront;
    public Material CardBack => cardBack;
    public string Instruction => instruction;
    public string FollowUp => followUp;
    public string Quote => quote;
    public Sprite Artwork => artwork;
}
