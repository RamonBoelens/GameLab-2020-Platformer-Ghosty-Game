﻿using System.Collections.Generic;
using UnityEngine;
public class CSVScriptReader : MonoBehaviour
{
    private List<Card> cards = new List<Card>();

    public List<Card> ReadCardDatabase(TextAsset databaseFile)
    {
        // Split every line in the file
        string[] data = databaseFile.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ';' });

            if (row[0] != "")
            {
                // Create the card and fill in all the information
                Card card = new Card();

                card.SetCardType(row[0]);
                int.TryParse(row[1], out card.pointsValue);
                card.culture = row[2];
                card.txt_Front = row[3];
                card.txt_Instruction = row[4];
                card.txt_Qoute = row[5];
                card.txt_Translation = row[6];
                card.txt_QouteAssosiation = row[7];
                card.txt_Back = row[8];
                card.txt_AnswerA = row[9];
                card.txt_AnswerB = row[10];
                card.txt_AnswerC = row[11];
                card.CorrectAnswer = row[12];
                int.TryParse(row[13], out card.uniqueAnimationID);

                cards.Add(card);
            }
        }

        Debug.Log("Database succesfully loaded!");

        return cards;
    }

    public List<Card> GetCards()
    {
        return cards;
    }
}
