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
    public Category category;
    [TextArea(3, 10)]
    public string description;
    public Sprite artwork;
    public int points;

    public int GetCategoryNumber()
    {
        if (category == Category.DiversiCHOICE)
            return 1;
        else if (category == Category.DiversiRISK)
            return 2;
        else if (category == Category.DiversiSHARE)
            return 3;
        else if (category == Category.DiversiSMART)
            return 4;
        else if (category == Category.DiversiGUIDE)
            return 5;
        else
            return 0;
    }
}
