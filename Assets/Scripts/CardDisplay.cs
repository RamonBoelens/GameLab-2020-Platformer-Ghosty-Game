﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [Header("Card Front")]
    public TextMeshProUGUI txt_Description;
    public TextMeshProUGUI txt_Instruction;
    public TextMeshProUGUI txt_Quote;
    public TextMeshProUGUI txt_Answers;
    public Image artwork;

    [Header("Card Back")]
    public TextMeshProUGUI txt_Explanation;

    private Card currentCard;
    private Material[] prefabMaterials;
    private Material[] thisCardMaterials;



    [Header("Card Slots")]
    public diversiChoiceCard choiceCard;
    public diversiGuideCard  guideCard;
    public diversiRiskCard   riskCard;
    public diversiShareCard  shareCard;
    public diversiSmartsCard smartsCard;

    [Header("Card Front")]
    public TextMeshProUGUI textInstructionFrontSlot;
    public TextMeshProUGUI textAnswersFrontSlot;
    //public TextMeshProUGUI textFollowUpFrontSlot;
    //public Image artwork;

    [Header("Card Back")]
    public TextMeshProUGUI textInstructionBackSlot;
    //public TextMeshProUGUI textFollowUpBackSlot;

    private void Awake()
    {
        prefabMaterials = GetComponent<MeshRenderer>().materials;
        thisCardMaterials = new Material[2];
    }

    public void UpdateCard(Card card)
    {
        currentCard = card;
    }

    public void UpdateChoiceCard(diversiChoiceCard card)
    {
        // Empty the other card slots
        guideCard = null;
        riskCard = null;
        shareCard = null;
        smartsCard = null;

        // Empty the textfields we don't need
        //textFollowUpFrontSlot.text = null;

        // Add card to the card slot
        choiceCard = card;

        // Update the other fields
        textInstructionFrontSlot.text = card.Instruction;
        textAnswersFrontSlot.text = "True \nFalse";
        textInstructionBackSlot.text = card.BCK_Answer;
        //textFollowUpBackSlot.text = card.BCK_FollowUp;

        // Update image if there is one
        if (card.Artwork != null)
        {
            artwork.sprite = card.Artwork;
        }
        // Else hide the image component
        else
        {
            artwork.enabled = false;
        }

        // Update the card material
        thisCardMaterials = prefabMaterials;
        thisCardMaterials[0] = new Material(card.CardBack);
        thisCardMaterials[1] = new Material(card.CardFront);
        GetComponent<MeshRenderer>().materials = thisCardMaterials;
    }

    public void UpdateGuideCard(diversiGuideCard card)
    {
        // Empty the other card slots
        choiceCard = null;
        riskCard = null;
        shareCard = null;
        smartsCard = null;

        // Empty the textfields we don't need
        textAnswersFrontSlot.text = null;
        //textFollowUpBackSlot.text = null;
        textInstructionBackSlot.text = null;

        // Add card to the card slot
        guideCard = card;

        // Update the other fields
        textInstructionFrontSlot.text = card.Instruction;
        //textFollowUpFrontSlot.text = card.FollowUp;

        // Update image if there is one
        if (card.Artwork != null)
        {
            artwork.sprite = card.Artwork;
        }
        // Else hide the image component
        else
        {
            artwork.enabled = false;
        }

        // Update the card material
        thisCardMaterials = prefabMaterials;
        thisCardMaterials[0] = new Material(card.CardBack);
        thisCardMaterials[1] = new Material(card.CardFront);
        GetComponent<MeshRenderer>().materials = thisCardMaterials;
    }

    public void UpdateRiskCard(diversiRiskCard card)
    {
        // Empty the other card slots
        choiceCard = null;
        guideCard = null;
        shareCard = null;
        smartsCard = null;

        // Empty the textfields we don't need
        textAnswersFrontSlot.text = null;
        //textFollowUpBackSlot.text = null;
        textInstructionBackSlot.text = null;

        // Add card to the card slot
        riskCard = card;

        // Update the other fields
        textInstructionFrontSlot.text = card.Instruction;
        //textFollowUpFrontSlot.text = card.FollowUp;

        // Update image if there is one
        if (card.Artwork != null)
        {
            artwork.sprite = card.Artwork;
        }
        // Else hide the image component
        else
        {
            artwork.enabled = false;
        }

        // Update the card material
        thisCardMaterials = prefabMaterials;
        thisCardMaterials[0] = new Material(card.CardBack);
        thisCardMaterials[1] = new Material(card.CardFront);
        GetComponent<MeshRenderer>().materials = thisCardMaterials;
    }
    public void UpdateShareCard(diversiShareCard card)
    {
        // Empty the other card slots
        choiceCard = null;
        guideCard = null;
        riskCard = null;
        smartsCard = null;

        // Empty the textfields we don't need
        textAnswersFrontSlot.text = null;
        //textFollowUpBackSlot.text = null;
        textInstructionBackSlot.text = null;

        // Add card to the card slot
        shareCard = card;

        // Update the other fields
        textInstructionFrontSlot.text = card.Instruction;
        //textFollowUpFrontSlot.text = card.FollowUp;

        // Update image if there is one
        if (card.Artwork != null)
        {
            artwork.sprite = card.Artwork;
        }
        // Else hide the image component
        else
        {
            artwork.enabled = false;
        }

        // Update the card material
        thisCardMaterials = prefabMaterials;
        thisCardMaterials[0] = new Material(card.CardBack);
        thisCardMaterials[1] = new Material(card.CardFront);
        GetComponent<MeshRenderer>().materials = thisCardMaterials;
    }

    public diversiChoiceCard GetChoiceCard()
    {
        return choiceCard;
    }

    public diversiGuideCard GetGuideCard()
    {
        return guideCard;
    }

    public diversiRiskCard GetRiskCard()
    {
        return riskCard;
    }

    public diversiShareCard GetShareCard()
    {
        return shareCard;
    }

    public diversiSmartsCard GetSmartsCard()
    {
        return smartsCard;
    }
}
