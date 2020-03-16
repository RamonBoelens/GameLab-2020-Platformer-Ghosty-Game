using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/DiversiSHARE Card")]
public class diversiShareCard : ScriptableObject
{
    [Header("Basic Information.")]
    [SerializeField] private new string name;

    [Header("Front of the card.")]
    [SerializeField] private Sprite artwork;
    [TextArea(3, 10)] [SerializeField] private string instruction;
    [TextArea(3, 10)] [SerializeField] private string followUp;

    private int points = 5;


    public int Points => points;
    public string Name => name;
    public string Instruction => instruction;
    public string FollowUp => followUp;
    public Sprite Artwork => artwork;
}
