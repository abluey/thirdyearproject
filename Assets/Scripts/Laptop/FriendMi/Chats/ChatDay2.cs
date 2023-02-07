using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatDay2 : MonoBehaviour {
    
    void OnEnable() {
        ChatManager.chatbox.text = PlayerChoices.chatRecord;

        if (PlayerPrefs.GetInt("TimeCount") == 0) {
            switch (PlayerChoices.chatProgress) {
                case 0: T0_Start(); break;
                case 1: break;
                case 2: T0_WhereToWhy(); break;
                case 3: T0_Who(); break;
                default: Debug.Log("Something went wrong D2T0"); break;
            }
        } else if (PlayerPrefs.GetInt("TimeCount") == 1) {
            switch (PlayerChoices.chatProgress) {
                case 0: T1_Start(); break;
                case 1: break;
                default: Debug.Log("Something went wrong D2T1"); break;
            }
        } else {
            switch (PlayerChoices.chatProgress) {
                case 0: T2_Start(); break;
                case 1: break;
                default: Debug.Log("Something went wrong D2T2"); break;
            }
        }
    }

    /***
    TIME 0
    ***/

    private void T0_Start() {
        ChatManager.chatbox.text = "Day 2\nReese: It's been a pleasure talking with you. You won't see me after tomorrow.\n";

        ChatManager.choice1text.text = "Why not?";
        ChatManager.choice2text.text = "Where are you going?";

        ChatManager.choice1.onClick.AddListener(T0_Why);
        ChatManager.choice2.onClick.AddListener(T0_Where);
        ChatManager.ShowChoices(true);
    }

    private void T0_Why() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: Why not?\n");
        StartCoroutine(T0_Leave());
    }
    
    private void T0_Where() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: Where are you going?\n");
        StartCoroutine(T0_WhereReply());
    }

    private IEnumerator T0_WhereReply() {
        yield return StartCoroutine(ChatManager.IsTyping(1.0f));
        ChatManager.updateChatRecords("\nReese: Away.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nCan't say much.\n");

        PlayerChoices.chatProgress = 2;
        T0_WhereToWhy();
    }

    private void T0_WhereToWhy() {
        ChatManager.choice1text.text = "Why?";
        ChatManager.choice1.gameObject.SetActive(true);
        ChatManager.choice1.onClick.AddListener(T0_WhereToWhyBridge);
    }

    private void T0_WhereToWhyBridge() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: Why?\n");
        StartCoroutine(T0_Leave());
    }

    private IEnumerator T0_Leave() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: Because they found me.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nI'm sorry I can't tell you more.\n");

        PlayerChoices.chatProgress = 3;
        T0_Who();
    }

    private void T0_Who() {
        ChatManager.choice1text.text = "Who found you?";
        ChatManager.choice1.gameObject.SetActive(true);
        ChatManager.choice1.onClick.AddListener(T0_WhoReply);
    }

    private void T0_WhoReply() {
        ChatManager.updateChatRecords("\nYou: Who found you?\n");
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        PlayerChoices.chatProgress = 1;
    }

    /***
    TIME 1
    ***/
    
    private void T1_Start() {

    }

    /***
    TIME 2
    ***/
    
    private void T2_Start() {

    }
}