﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;
    public TalkManager talkManager;
    public int talkIndex;
    public Image portraitImg;
    public QuestManager questManager;

    void Start() 
    {
        Debug.Log(questManager.CheckQuest());
    }

    public void Action(GameObject scanObj)
    {
        // Get Current Object
        scanObject = scanObj;
        ObjectData objectData = scanObj.GetComponent<ObjectData>();
        Talk(objectData.id, objectData.isNpc);

        // Visible Talk for Action
        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        // Set Talk Data
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        // End Talk
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }

        // Continue Talk
        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0];

            // Show Portrait
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }
}
