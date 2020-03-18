using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public CardDisplay cardDisplay;

    public List<diversiChoiceCard> choiceCards;
    public List<diversiGuideCard> guideCards;
    public List<diversiRiskCard> riskCards;
    public List<diversiShareCard> shareCards;
    public List<diversiSmartsCard> smartsCards;

    private void Start()
    {
        cardDisplay.UpdateChoiceCard(choiceCards[0]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            cardDisplay.UpdateChoiceCard(choiceCards[0]);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            cardDisplay.UpdateGuideCard(guideCards[0]);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            cardDisplay.UpdateRiskCard(riskCards[0]);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            cardDisplay.UpdateShareCard(shareCards[0]);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            //cardDisplay.UpdateSmartsCard(smartsCards[0]);
        }
    }
}
