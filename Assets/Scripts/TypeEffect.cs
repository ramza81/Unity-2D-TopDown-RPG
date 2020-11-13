﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public int CharPerSeconds;
    public GameObject EndCursor;
    public bool isAni;
    
    Text msgText;
    AudioSource audioSource;

    string targetMsg;
    int index;
    float interval;


    private void Awake() 
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMsg(string msg)
    {
        if (isAni)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        // Start Animation
        interval = 1.0f / CharPerSeconds;        

        isAni = true;
        Invoke("Effecting", 1 / CharPerSeconds);
    }

    void Effecting()
    {
        // End Animation
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index];

        // Sound
        if (targetMsg[index] != ' ' || targetMsg[index] != '.')
        {
            audioSource.Play();            
        }     
        
        index++;

        // Recursive
        Invoke("Effecting", interval);
    }    

    void EffectEnd()
    {
        isAni = false;
        EndCursor.SetActive(true);
    }      
}
