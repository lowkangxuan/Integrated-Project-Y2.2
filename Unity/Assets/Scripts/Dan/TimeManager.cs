using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
Author: Dan

Name of Class: TimeManager

Description of Class: Basic Timer for the outdoor shooting

Date Created: 3 / 02 / 2022
*/
public class TimeManager : MonoBehaviour
{

    public TMP_Text timeElapsedDisplay;
    //public TMP_Text fpsTxt;
    public TMP_Text timeRemainingDisplay;
    public GameObject startButton;
    public float time;

    [SerializeField]
    private float timeRemaining = 45;
    private float msec;
    private float sec;
    private float min;

    private float currentSec;
    private float timeLimit = 0;
    //public bool isDead;

    private string resetText = "00:00:00";

    private ActivateTargets activateTargets;
    private GameManager gameManager;

    

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        activateTargets = FindObjectOfType<ActivateTargets>();
    }

    

    IEnumerator StopWatch()
    {
        while (true)
        {
            

            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                time += Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                time = timeLimit;
                timeRemainingDisplay.color = Color.red;
                timeElapsedDisplay.color = Color.red;

                //gameManager.GameOver();
                //stop spawning targets once game is over
                activateTargets.stopOutdoor();
                gameManager.UpdatePlayerOutdoorStats();
                StopTimer();
                // can store the points to firebase here once the game finishes
                 


            }
            yield return null;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
       
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
            time = timeLimit;
            Debug.Log("time limit" + timeLimit);
            
        }
        float min = Mathf.FloorToInt(timeToDisplay / 60);
        float sec = Mathf.FloorToInt(timeToDisplay % 60);//removes the mins
    
        float msec = (timeToDisplay % 1) * 1000;//Mathf.FloorToInt((timeRemaining - sec) * 100);
        Debug.Log("msec... " + msec);
        timeRemainingDisplay.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);

        Debug.LogFormat("Time {0}/// {1}: {2}", time, (int)time, timeLimit);

        
        msec = Mathf.FloorToInt((time - (int)time) * 100);
        sec = Mathf.FloorToInt(time % 60);
        min = Mathf.FloorToInt(time / 60);
        currentSec = sec;

        timeElapsedDisplay.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);


    }


    public void StartTimer()
    {
        if (timeRemaining == 0)
        {
           StopWatchReset();
           gameManager.OutdoorPoints = 0;
        }
        startButton.SetActive(false);
        StartCoroutine("StopWatch");
    }


    public void StopTimer()
    {
        startButton.SetActive(true);
        StopCoroutine("StopWatch");
    }

    public void StopWatchReset()
    {
        timeRemaining = 30;
        timeRemainingDisplay.text = resetText;
        timeRemainingDisplay.color = Color.white;

        timeElapsedDisplay.text = resetText;
        timeElapsedDisplay.color = Color.white;
     }

    public void SetTimeRemaining(float newTimeRemaing)
    {
        timeRemaining = newTimeRemaing;
        timeLimit = newTimeRemaing;
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    public float GetCurrentSec()
    {
        return currentSec;
    }


}
