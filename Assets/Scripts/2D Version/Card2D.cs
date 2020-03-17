using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TwoDCategory
{
    DiversiCHOICE,
    DiversiRISK,
    DiversiSHARE,
    DiversiSMART,
    DiversiGUIDE,
}

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/2D/Card")]
public class Card2D : ScriptableObject
{
    [SerializeField] private TwoDCategory category;
    [TextArea(3, 10)]
    [SerializeField] private string description;
    [SerializeField] private Sprite artwork;

    public TwoDCategory Category => category;
    public string Description => description;
    public Sprite Artwork => artwork;
}