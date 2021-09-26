using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DictionaryData
{
    public int scorePoints;
    public int currentLineNumber;
    public int seconds;
    public int minutes;
    public int hours;
    public int nonResetedSeconds;
    public bool formule1Played;
    public bool parfaitPlayed;


    public DictionaryData (WordDictionaryManager playerDat)
    {
        scorePoints = playerDat.scorePoints;
        currentLineNumber = playerDat.currentLineNumber;
        seconds = playerDat.seconds;
        minutes = playerDat.minutes;
        hours = playerDat.hours;
        nonResetedSeconds = playerDat.nonResetedSeconds;
        formule1Played = playerDat.formule1Played;
        parfaitPlayed = playerDat.parfaitPlayed;

    }

}
