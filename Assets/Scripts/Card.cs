using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Category
{
    Cat1,
    Cat2,
    Cat3,
    Cat4,
    Cat5,
}

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public Category category;
    public string description;
    public Sprite artwork;

    public int GetCategoryNumber()
    {
        if (category == Category.Cat1)
            return 1;
        else if (category == Category.Cat2)
            return 2;
        else if (category == Category.Cat3)
            return 3;
        else if (category == Category.Cat4)
            return 4;
        else if (category == Category.Cat5)
            return 5;
        else
            return 0;
    }
}
