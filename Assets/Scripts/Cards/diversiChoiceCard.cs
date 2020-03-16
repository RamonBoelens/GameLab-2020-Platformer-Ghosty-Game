﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/DiversiCHOICE Card")]
public class diversiChoiceCard : ScriptableObject
{
    [Header("Basic Information.")]
    [SerializeField] private new string name;

    [Header("Front of the card.")]
    [SerializeField] private Sprite artwork;
    [TextArea(3, 10)] [SerializeField] private string instruction;

    [Header("Back of the card.")]
    [TextArea(3, 10)] [SerializeField] private string bck_Answer;
    [TextArea(3, 10)] [SerializeField] private string bck_FollowUp;

    [Header("Answer.")]
    [Tooltip("If the answer is true then check the box. \nIf the answer is false then uncheck it.")]
    [SerializeField] private bool answer;
    

    private int points = 3;

    public int Points => points;
    public string Name => name;
    public string Instruction => instruction;
    public string BCK_Answer => bck_Answer;
    public string BCK_FollowUp => bck_FollowUp;
    public bool Answer => answer;
    public Sprite Artwork => artwork;
}
