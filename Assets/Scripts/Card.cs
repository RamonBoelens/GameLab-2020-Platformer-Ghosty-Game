using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Category
{
    DiversiCHOICE,
    DiversiRISK,
    DiversiSHARE,
    DiversiSMART,
    DiversiGUIDE,
}

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    [SerializeField] private Category category;
    [TextArea(3, 10)]
    [SerializeField] private string description;
    [SerializeField] private Sprite artwork;

    public Category Category => category;
    public string Description => description;
    public Sprite Artwork => artwork;
}
