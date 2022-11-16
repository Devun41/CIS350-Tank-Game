﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //static public variables
    public static bool gameOver = false;
    public static bool player1Win = false;
    public static int currentScore;

    [Header("Public Facing Variables:")]
    //non-static public variables
    public Image[] halves;
    private int numHalves;
    public int maxScore;

    [Header("Debugging Variables:")]
    //debug variables
    public bool debugGameOver;
    public bool debugPlayer1Win;
    public int debugScoreboard;
    public float debugScoreRatio;
    public float[] debugReferenceRatios;


    // Start is called once per script
    void Start()
    {
        //get the length of halves array and store it so we dont have to get the Length call each time
        numHalves = halves.Length;

        //make sure the max score is set in the inspector and is above the minimum value which is the numHalves
        if (maxScore < numHalves)
        {
            Debug.LogError("[GameManager.cs] - (int) maxScore not set in inspector! (Hint: maxScore should be >= " + numHalves + ")");
        }

        //set the currentScore equal to the maxScore / 2 for both the players;
        currentScore = maxScore / 2;
        debugScoreboard = currentScore;
        debugReferenceRatios = new float[numHalves];
        debuggingMethod();
    }

    // Update is called once per frame
    void Update()
    {
        //for the debugging in the inspector
        currentScore = debugScoreboard;
        debugGameOver = gameOver;
        debugPlayer1Win = player1Win;
        debugScoreRatio = (float)currentScore / (float)maxScore;


        //logic for scoreboard
        if (!gameOver)
        {
            if (currentScore >= maxScore)
            {
                halves[numHalves - 1].GetComponent<Image>().enabled = true;
                player1Win = true;
                gameOver = true;
            }
            else if (currentScore <= 0)
            {
                halves[0].GetComponent<Image>().enabled = false;
                gameOver = true;
            }
            else
            {
                for (int i = numHalves - 1; i > 0; i--)
                {
                    if (((float)currentScore / (float)maxScore) < ((float)(i + 1) / (float)numHalves))
                    {
                        halves[i].GetComponent<Image>().enabled = false;
                        if (i == 0)
                        {
                            halves[i].GetComponent<Image>().enabled = true;
                        }
                    }
                    else
                    {
                        halves[i].GetComponent<Image>().enabled = true;
                    }
                }
            }
        }
    }

    // called once on start
    void debuggingMethod()
    {
        for (int i = 0; i < debugReferenceRatios.Length; i++)
        {
            debugReferenceRatios[i] = (float)i / (float)numHalves;
        }
    }
}
