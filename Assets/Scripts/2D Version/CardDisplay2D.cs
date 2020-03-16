using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay2D : MonoBehaviour
{

    public Card2D card;
    public Image cardImageSlot;
    public Image artworkImageSlot;
    public TextMeshProUGUI descriptionText;

    public Sprite[] cardTemplates;

    // Start is called before the first frame update
    void Start()
    {
        InitCardValues();
    }

    private void InitCardValues()
    {
        // Setup Images
        SetupCardTemplate(card.Category);
        artworkImageSlot.sprite = card.Artwork;

        // Setup Text
        descriptionText.text = card.Description;
    }

    private void SetupCardTemplate(Category cat)
    {
        if (cat == Category.DiversiCHOICE)
        {
            cardImageSlot.sprite = cardTemplates[0];
        }
        else if (cat == Category.DiversiGUIDE)
        {
            cardImageSlot.sprite = cardTemplates[1];
        }
        else if (cat == Category.DiversiRISK)
        {
            cardImageSlot.sprite = cardTemplates[2];
        }
        else if (cat == Category.DiversiSHARE)
        {
            cardImageSlot.sprite = cardTemplates[3];
        }
        else if (cat == Category.DiversiSMART)
        {
            cardImageSlot.sprite = cardTemplates[4];
        }
        else
        {
            Debug.LogError("Can't setup the card template because the card has no category!");
        }
    }

    public void UpdateCard()
    {
        InitCardValues();
    }
}
