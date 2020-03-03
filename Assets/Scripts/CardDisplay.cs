using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{

    public Card card;

    public TextMeshProUGUI categoryText;

    public Image artworkImage;

    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI catergoryNumberText;

    public Image cardTemplateImage;

    private int category;


    // Start is called before the first frame update
    void Start()
    {
        category = card.GetCategoryNumber();

        InitCardValues();
        SetupCardColor();
    }

    private void InitCardValues()
    {
        categoryText.text = card.category.ToString();

        artworkImage.sprite = card.artwork;

        descriptionText.text = card.description;
        catergoryNumberText.text = category.ToString();
    }

    private void SetupCardColor()
    {
        if (category == 0)
        {
            Debug.LogWarning("Card category isn't initialized.");
            return;
        }
        else if (category == 1)
        {
            cardTemplateImage.color = Color.blue;
        }
        else if (category == 2)
        {
            cardTemplateImage.color = Color.cyan;
        }
        else if (category == 3)
        {
            cardTemplateImage.color = Color.green;
        }
        else if (category == 4)
        {
            cardTemplateImage.color = Color.magenta;
        }
        else if (category == 5)
        {
            cardTemplateImage.color = Color.yellow;
        }
    }
}
