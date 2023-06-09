﻿/*
* Zach Wilson
* CIS 350 - Group Project
* This script controls the mute button for Audio during the game 
* Note: Name of script kind of inaccurate now but I dont wanna try and mess with it for now... A better name for it would be MuteToggleManager
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    Toggle muteToggle;
    public Text mText;
    public Image mutedIconSprite;
    bool mutedIconSpriteSet;
    public bool unMutedBoolDebug = GlobalSettings.bUnMuted;
    private bool mTextSet = true;

    // Start is called before the first frame update
    void Start()
    {
        //get the toggle object
        muteToggle = GameObject.FindWithTag("MuteToggle").GetComponent<Toggle>() as Toggle;
        //start the listener for a value change
        muteToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(muteToggle);
        });
        //get the muted sprite (or isOn == false sprite that has to be managed by this script)
        if (mutedIconSprite == null)
        {
            try
            {
                mutedIconSprite = GameObject.FindWithTag("MutedSprite").GetComponent<Image>() as Image;
                mutedIconSpriteSet = true;
            }
            catch
            {
                Debug.Log("No Sound Muted Icon Sprite Found!");
                mutedIconSpriteSet = false;
            }
        }
        else
        {
            mutedIconSpriteSet = true;
        }

        if(mText == null)
        {
            mTextSet = false;
        }

        //start the muteToggle isOn variable at what the global variable is set to
        muteToggle.isOn = GlobalSettings.bUnMuted;

        //update the properties of the toggle appropriately
        ToggleValueChanged(muteToggle);
    }

    void ToggleValueChanged(Toggle change)
    {
        GlobalSettings.bUnMuted = muteToggle.isOn;
        unMutedBoolDebug = GlobalSettings.bUnMuted;
        if (muteToggle.isOn)
        {
            if (mTextSet) { mText.text = "Sound On"; }
            if (mutedIconSpriteSet)
            {
                mutedIconSprite.enabled = false;
            }
        }
        else
        {
            if (mTextSet)
            {
                mText.text = "Sound Off";
            }
            if (mutedIconSpriteSet)
            {
                mutedIconSprite.enabled = true;
            }
        }
    }
}
