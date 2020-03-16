using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/DiversiGUIDE Card")]
public class diversiGuideCard : ScriptableObject
{
    [Header("Basic Information.")]
    [SerializeField] private new string name;

    [Header("Front of the card.")]
    [SerializeField] private Sprite artwork;
    [TextArea(3, 10)] [SerializeField] private string instruction;
    [TextArea(3, 10)] [SerializeField] private string followUp;
    [TextArea(3, 10)] [SerializeField] private string quote;

    private int points = 1;


    public int Points => points;
    public string Name => name;
    public string Instruction => instruction;
    public string FollowUp => followUp;
    public string Quote => quote;
    public Sprite Artwork => artwork;
}
