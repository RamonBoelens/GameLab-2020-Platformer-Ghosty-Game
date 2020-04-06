using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardTypes { Choice, Guide, Risk, Share, Smarts};

public class Card
{
    public CardTypes cardType;
    public int pointsValue;
    public string culture;
    public string txt_Front;
    public string txt_Instruction;
    public string txt_Qoute;
    public string txt_Translation;
    public string txt_QouteAssosiation;
    public string txt_Back;
    public string txt_AnswerA;
    public string txt_AnswerB;
    public string txt_AnswerC;
    public string CorrectAnswer;
    public int uniqueAnimationID;

    public void SetCardType(string type)
    {
        if (type == "Choice")
        {
            cardType = CardTypes.Choice;
        } else if (type == "Guide")
        {
            cardType = CardTypes.Guide;
        } else if (type == "Risk")
        {
            cardType = CardTypes.Risk;
        } else if (type == "Share")
        {
            cardType = CardTypes.Share;
        } else if (type == "Smarts")
        {
            cardType = CardTypes.Smarts;
        } else
        {
            Debug.LogError("Couldn't find the type " + type);
        }
    }
}
