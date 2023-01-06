using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatDay1 : MonoBehaviour {

    void OnEnable() {
        if (PlayerPrefs.GetInt("TimeCount") == 0) {
            switch (PlayerChoices.chatProgress) {
                case 0: T0_Start(); break;
                case 1: break; //finished
            }
        } else if (PlayerPrefs.GetInt("TimeCount") == 1) {
            // switch
        } else if (PlayerPrefs.GetInt("TimeCount") == 2) {
            // switch
            // ask for plant
        } else {
            Debug.Log("Something went wrong");
        }
        
    }

    private void T0_Start() {
        ChatManager.chatbox.text = "Day 1\nEnter random philsophical thought here.";
        // cannot update chatRecords here or else it'll duplicate it so many times
        
        ChatManager.choice1text.text = "Choice 1";
        ChatManager.choice2text.text = "Choice 2";

        ChatManager.ShowChoices(true);

        ChatManager.choice1.onClick.AddListener(T0_Reply1);
        ChatManager.choice2.onClick.AddListener(T0_Reply2);
    }

    private void T0_Reply1() {
        ChatManager.ResetListeners();
        PlayerChoices.chatRecord += "\n\n" + ChatManager.chatbox.text;
        ChatManager.ShowChoices(false);

    }

    private void T0_Reply2() {
        ChatManager.ResetListeners();
        PlayerChoices.chatRecord += "\n\n" + ChatManager.chatbox.text;
        ChatManager.ShowChoices(false);
    }

}