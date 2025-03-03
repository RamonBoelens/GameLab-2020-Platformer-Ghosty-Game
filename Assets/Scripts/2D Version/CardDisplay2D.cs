﻿using System.Collections;
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

    private void SetupCardTemplate(TwoDCategory cat)
    {
        if (cat == TwoDCategory.DiversiCHOICE)
        {
            cardImageSlot.sprite = cardTemplates[0];
        }
        else if (cat == TwoDCategory.DiversiGUIDE)
        {
            cardImageSlot.sprite = cardTemplates[1];
        }
        else if (cat == TwoDCategory.DiversiRISK)
        {
            cardImageSlot.sprite = cardTemplates[2];
        }
        else if (cat == TwoDCategory.DiversiSHARE)
        {
            cardImageSlot.sprite = cardTemplates[3];
        }
        else if (cat == TwoDCategory.DiversiSMART)
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
