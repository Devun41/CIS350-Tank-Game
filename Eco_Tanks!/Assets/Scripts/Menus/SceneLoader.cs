﻿/*
* Devun Schneider
* CIS 350 - Trash Pick-Up Simulator
* This script manages the changing of scenes throughout the menus
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //to determine the num of players and what scripts need to be enabled
    bool b_aiEnabled = false;
    bool b_p2Enabled = false;

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Time.timeScale = 1.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void LoadScene(string sceneName)
    {
        Debug.Log("SceneLoader");
        Debug.Log("sceneName to load: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //Zach Wilson From Here On for sending the player to the right scene with the right settings enabled
    public void StartGame(string sceneName)
    {
        if (GlobalSettings.hasSeenTutorial)
        {
            Debug.Log("SceneLoader");
            Debug.Log("sceneName to load: " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            try
            {
                GlobalSettings.hasSeenTutorial = true;
                Debug.Log("SceneLoader");
                Debug.Log("sceneName to load: " + GlobalSettings.tutorialScene);
                if (SceneManager.GetSceneByName(GlobalSettings.tutorialScene).IsValid())
                {
                    SceneManager.LoadScene(GlobalSettings.tutorialScene);
                }
                else
                {
                    throw new UnityException("Tutorial Scene Not Found!");
                }
            }
            catch
            {
                GlobalSettings.hasSeenTutorial = false;
                Debug.Log("SceneLoader Failed to find Tutorial Scene: " + GlobalSettings.tutorialScene);
                Debug.Log("SceneLoader");
                Debug.Log("sceneName to load: " + sceneName);
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    public void p2Enabled(bool enabled)
    {
        b_p2Enabled = enabled;
    }
    public void aiEnabled(bool enabled)
    {
        b_aiEnabled = enabled;
    }
    public void updateSettingsAndStartGame(string sceneName)
    {
        if (b_aiEnabled && b_p2Enabled)
        {
            b_p2Enabled = false;
        }

        GlobalSettings.player2AIEnabled = b_aiEnabled;
        GlobalSettings.player2Enabled = b_p2Enabled;

        StartGame(sceneName);
    }
}