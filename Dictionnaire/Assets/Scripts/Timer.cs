using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeText;
    private GameObject thisWordDicoManager;

    public int seconds = 0;
    public int minutes = 0;
    public int hours = 0;

    private string secondsString;
    private string minutesString;
    private string hoursString;

    [HideInInspector]public int nonResetedSeconds;

    private bool hasPassed = true;

    void Start()
    {
        hasPassed = true;

        seconds = this.GetComponent<WordDictionaryManager>().seconds;
        minutes = this.GetComponent<WordDictionaryManager>().minutes;
        hours = this.GetComponent<WordDictionaryManager>().hours;
    }

    void Update()
    {
        ConvertMinutes();
        ConvertHours();

        if(hasPassed)
        {
            StartCoroutine(TimeCount());
            hasPassed = false;
        }

        //Better display of time

        if(seconds < 10)
        {
            secondsString = "0" + seconds;
        }
        else
        {
            secondsString = seconds.ToString();
        }

        if(minutes < 10)
        {
            minutesString = "0" + minutes;
        }
        else
        {
            minutesString = minutes.ToString();
        }
        
        if(hours < 10)
        {
            hoursString = "0" + hours;
        }
        else
        {
            hoursString = hours.ToString();
        }


        //Update time text
        
        timeText.text = hoursString + ":" +minutesString + ":" + secondsString;
    }

    IEnumerator TimeCount()
    {        
        seconds++;
        nonResetedSeconds++;

        yield return new WaitForSeconds(1f);

        hasPassed = true;
    }

    void ConvertMinutes()
    {
        if(seconds == 60)
        {
            seconds = 0;
            minutes++;
        }
    }

    void ConvertHours()
    {
        if(minutes == 60)
        {
            minutes = 0;
            hours++;
        }
    }

}
