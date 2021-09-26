using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class WordDictionaryManager : MonoBehaviour
{
    [Header("Selected Dictionary")]
    public TextAsset dictionnaire;
    public Text currentLetter;

    [Header("Setup Texts")]
    public InputField inputField;
    public InputField currentInputFieldText;

    public Text scoreText;
    [HideInInspector]public int scorePoints;

    public Text wordLevelText;
    private int wordLevel;

    public Text wordPerMinutesText;
    private double wordPerMinutes;

    [Header("Text to display")]
    public Text previousPreviousText;
    private string[] previousPreviousLines;

    public Text previousText;
    private string[] previousLines;

    public Text currentText;
    [HideInInspector]public int currentLineNumber;
    private string[] currentLines;

    public Text nextText;
    private string[] nextLines;

    public Text nextNextText;
    private string[] nextNextLines;

    [Header("Bonus")]
    public AudioClip sardFormule1;
    public AudioClip sardParfait;

    //Cache variables
    private AudioSource cameraAudioSource;

    private double time;

    private bool isSame = false;
    [HideInInspector] public bool formule1Played = false;
    [HideInInspector] public bool parfaitPlayed = false;
    private bool hasUpdated;

    [HideInInspector] public int seconds;
    [HideInInspector] public int minutes;
    [HideInInspector] public int hours;
    [HideInInspector] public int nonResetedSeconds;

    private Timer timer;

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        DictionaryData data = SaveSystem.LoadPlayer();

        currentLineNumber = data.currentLineNumber;
        scorePoints = data.scorePoints;
        seconds = data.seconds;
        minutes = data.minutes;
        hours = data.hours;
        nonResetedSeconds = data.nonResetedSeconds;
        formule1Played = data.formule1Played;
        parfaitPlayed = data.parfaitPlayed;
    }

    void Start()
    {
        LoadPlayer();

        timer = GetComponent<Timer>();
        cameraAudioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();

        inputField.ActivateInputField();

        if(dictionnaire)
        {
            previousPreviousLines = (dictionnaire.text.Split('\n'));
            previousLines = (dictionnaire.text.Split('\n'));
            currentLines = (dictionnaire.text.Split('\n'));
            nextLines = (dictionnaire.text.Split('\n'));
            nextNextLines = (dictionnaire.text.Split('\n'));
        }
    }

    void Update()
    {
        SavePlayer();

        CheckTextInput();
        UpdateScoreUI();
        UpdateWordLevelUI();
        UpdateCurrentLetterUI();

        seconds = timer.seconds;
        minutes = timer.minutes;
        hours = timer.hours;
        nonResetedSeconds = timer.nonResetedSeconds;

        if(currentLineNumber < 0)
        {
            currentLineNumber = 0;
        }

        time = timer.nonResetedSeconds;

        if(wordLevel != 0 && time >= 0)
        {
            wordPerMinutes = wordLevel / (time / 60);
            wordPerMinutes = (int)wordPerMinutes;
        }

        wordPerMinutesText.text = wordPerMinutes + " : WPM";

        if(currentLineNumber >= 50 && !formule1Played)
        {
            cameraAudioSource.PlayOneShot(sardFormule1, 0.1f);
            formule1Played = true;
        }

        if(currentLineNumber >= 100 && !parfaitPlayed)
        {
            cameraAudioSource.PlayOneShot(sardParfait, 0.1f);
            parfaitPlayed = true;
        }

        //--------------------------[     All rendered texts     ]-------------------------------------

        if(currentLineNumber > 1)
        {
            string previousPrevious = previousPreviousLines[currentLineNumber - 2];
            previousPreviousText.text = previousPrevious.ToLower();
        }
        else
        {
            previousPreviousText.text = "";
        }

        if(currentLineNumber > 0)
        {
            string previous = previousLines[currentLineNumber - 1];
            previousText.text = previous.ToLower();
        }
        else
        {
            previousText.text = "";
        }

        string current = currentLines[currentLineNumber];
        currentText.text = current.ToLower();

        string next = nextLines[currentLineNumber + 1];
        nextText.text = next.ToLower();

        string nextNext = nextNextLines[currentLineNumber + 2];
        nextNextText.text = nextNext.ToLower();

        //------------------------------------------------------------------------------------------------

        if(isSame && !hasUpdated)
        {
            currentInputFieldText.GetComponent<Image>().color = Color.green;

            currentLineNumber++;

            BlankField(inputField);

            wordLevel++;
            scorePoints += 10;
            hasUpdated = true;
        }
        else
        {
            hasUpdated = false;
            currentInputFieldText.GetComponent<Image>().color = Color.white;
        }
    }

    void UpdateCurrentLetterUI()
    {
        string letter;
        letter = currentText.text.Substring(0, 1);

        currentLetter.text = letter.ToUpper();
    }   

    void CheckTextInput()
    {
        if(inputField.text.Trim() == currentText.text.Trim().ToLower())
        {
            isSame = true;
        }
        else
        {
            isSame = false;
        }
    }

    void BlankField(InputField input)
    {
        input.Select();
        input.text = "";
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score : " + scorePoints;
    }

    void UpdateWordLevelUI()
    {
        wordLevelText.text = currentLineNumber + " / 22739";
    }

}
