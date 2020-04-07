using System.Collections;
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

    [Header("Card Materials")]
    public Material[] choiceCard;
    public Material[] guideCard;
    public Material[] riskCard;
    public Material[] shareCard;
    public Material[] smartsCard;

    private Card currentCard;
    private Material[] prefabMaterials;
    private Material[] thisCardMaterials;

    private void Awake()
    {
        prefabMaterials = GetComponent<MeshRenderer>().materials;
        thisCardMaterials = new Material[2];
    }

    public void UpdateCard(Card card)
    {
        // Update the current card
        currentCard = card;
        // Empty all the text slots and the artwork slot and disable them
        ResetFields();

        // Check what card type it is because they will need different text slots
        if (card.cardType == CardTypes.Choice || card.cardType == CardTypes.Smarts)
        {
            // Enable the slots we need
            txt_Description.enabled = true;
            txt_Instruction.enabled = true;
            txt_Answers.enabled = true;
            txt_Explanation.enabled = true;

            // Fill out the fields with information
            txt_Description.text = card.txt_Front;
            txt_Instruction.text = card.txt_Instruction;
            txt_Explanation.text = card.txt_Back;

            // Fill out the answers slot
            if (card.txt_AnswerB != "-" || card.txt_AnswerB != "")
            {
                if (card.txt_AnswerC != "-" || card.txt_AnswerC != "")
                {
                    txt_Answers.text = card.txt_AnswerA + "\n" + card.txt_AnswerB + "\n" + card.txt_AnswerC;
                }
                else
                {
                    txt_Answers.text = card.txt_AnswerA + "\n" + card.txt_AnswerB;
                }
            }
            else
            {
                txt_Answers.text = card.txt_AnswerA;
            }            
        }
        else if (card.cardType == CardTypes.Risk || card.cardType == CardTypes.Share)
        {
            // Enable the slots we need
            txt_Description.enabled = true;
            txt_Instruction.enabled = true;

            // Fill out he fields with information
            txt_Description.text = card.txt_Front;
            txt_Instruction.text = card.txt_Instruction;
        }
        else if (card.cardType == CardTypes.Guide)
        {
            // Enable the slots we need
            txt_Instruction.enabled = true;
            txt_Quote.enabled = true;

            // Fill out he fields with information
            txt_Instruction.text = card.txt_Instruction;
            txt_Quote.text = card.txt_Qoute + "\n   - " + card.txt_QouteAssosiation; 
        }

        // Update the card material
        UpdateMaterial();

        // Check if the new card has an artwork if so enable it and set it
        if (card.uniqueAnimationID != 0)
        {
            Debug.Log("There is an animation on the card!");
        }
    }

    private void ResetFields()
    {
        // Emptying all the fields
        txt_Description.text = null;
        txt_Instruction.text = null;
        txt_Quote.text = null;
        txt_Answers.text = null;
        txt_Explanation.text = null;
        artwork.sprite = null;

        // Disabling all the fields
        txt_Description.enabled = false;
        txt_Instruction.enabled = false;
        txt_Quote.enabled = false;
        txt_Answers.enabled = false;
        txt_Explanation.enabled = false;
        artwork.enabled = false;
    }

    private void UpdateMaterial()
    {
        thisCardMaterials = prefabMaterials;

        if (currentCard.cardType == CardTypes.Choice)
        {
            thisCardMaterials[0] = new Material(choiceCard[0]);
            thisCardMaterials[1] = new Material(choiceCard[1]);
        }
        else if (currentCard.cardType == CardTypes.Guide)
        {
            thisCardMaterials[0] = new Material(guideCard[0]);
            thisCardMaterials[1] = new Material(guideCard[1]);
        }
        else if (currentCard.cardType == CardTypes.Risk)
        {
            thisCardMaterials[0] = new Material(riskCard[0]);
            thisCardMaterials[1] = new Material(riskCard[1]);
        }
        else if (currentCard.cardType == CardTypes.Share)
        {
            thisCardMaterials[0] = new Material(shareCard[0]);
            thisCardMaterials[1] = new Material(shareCard[1]);
        }
        else if (currentCard.cardType == CardTypes.Smarts)
        {
            thisCardMaterials[0] = new Material(smartsCard[0]);
            thisCardMaterials[1] = new Material(smartsCard[1]);
        }
        else
        {
            Debug.LogWarning("Can't apply the new card material because the card type " + currentCard.cardType + " doesn't exist!");
        }

        // Apply the new materials
        GetComponent<MeshRenderer>().materials = thisCardMaterials;
    }
}
