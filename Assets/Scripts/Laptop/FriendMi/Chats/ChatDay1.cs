using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatDay1 : MonoBehaviour {

    void OnEnable() {
        ChatManager.chatbox.text = PlayerChoices.chatRecord;

        if (PlayerPrefs.GetInt("TimeCount") == 0) {
            switch (PlayerChoices.chatProgress) {
                case 0:
                // checking if player profile is complete (not including Bio)
                    if (string.IsNullOrEmpty(PlayerPrefs.GetString("DOB")) ||
                        (PlayerPrefs.GetString("Gender") == "Prefer not to say")) {
                        T0_StartIncomplete();
                    }
                    else {
                        T0_StartComplete();
                    }
                    break;
                case 1: break;
                case 20: T0_Inc1Choice(); break;
                case 30: T0_Inc2(); break;
                default: Debug.Log("Something went wrong, D1T0."); break;
            }
        } else if (PlayerPrefs.GetInt("TimeCount") == 1) {
            if (!PlayerChoices.friendPrivacy && PlayerChoices.checkedFriendProfile) {
                // why they don't have profile

            } else {
                switch (PlayerChoices.ChatProgress) {
                   case 0: break; //weird there's only 1 groc store
                   default: Debug.Log("Something went wrong, D1T1."); break;
                }
            }
            
        } else if (PlayerPrefs.GetInt("TimeCount") == 2) {
            // switch
            // ask for plant
        } else {
            Debug.Log("Something went wrong");
        }
        
    }

    /***
    TIME 0: INCOMPLETE PROFILE
    ***/

    private void T0_StartIncomplete() {
        ChatManager.chatbox.text = "Day 1\nReese: Why don't you have your profile completed?\n";
        // cannot update chatRecords here or else it'll duplicate it so many times
        
        ChatManager.choice1text.text = "Why do you ask?";
        ChatManager.choice1.gameObject.SetActive(true);
        ChatManager.choice1.onClick.AddListener(T0_Inc);
    }

    private void T0_Inc() {
        ChatManager.ResetListeners();
        PlayerChoices.chatRecord += "\n\n" + ChatManager.chatbox.text;
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: Why do you ask?\n");

        StartCoroutine(T0_IncReply());
    }

    private IEnumerator T0_IncReply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));

        ChatManager.updateChatRecords("\nReese: Oh, just curious. People usually fill them in!\n");
        PlayerChoices.chatProgress = 20;
        T0_Inc1Choice();
    }

    private void T0_Inc1Choice() {
        ChatManager.choice1text.text = "I don't want to.";
        ChatManager.choice2text.text = "I must've forgot. I'll do it later.";
        ChatManager.ShowChoices(true);

        ChatManager.choice1.onClick.AddListener(T0_Inc1Choice1);
        ChatManager.choice2.onClick.AddListener(T0_Inc1Choice2);
    }

    private void T0_Inc1Choice1() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: I don't want to.\n");
        PlayerChoices.willUpdateProfile = 2;

        StartCoroutine(T0_Inc1Choice1Reply());
    }

    private void T0_Inc1Choice2() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: I must've forgot. I'll do it later.\n");
        PlayerChoices.willUpdateProfile = 1;

        StartCoroutine(T0_Inc1Choice2Reply());
    }

    private IEnumerator T0_Inc1Choice1Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(1.5f));

        ChatManager.updateChatRecords("\nReese: That's fair... To each their own!\n");
        PlayerChoices.chatProgress = 30;

        T0_Inc2();
    }

    private IEnumerator T0_Inc1Choice2Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(1.5f));

        ChatManager.updateChatRecords("\nReese: Oh, no pressure! I'm looking forward to it though!\n");
        PlayerChoices.chatProgress = 30;

        T0_Inc2();
    }

    private void T0_Inc2() {

        if (PlayerChoices.checkedFriendProfile) {
            ChatManager.choice1text.text = "Why don't you have yours filled in?";
            ChatManager.choice1.gameObject.SetActive(true);
            ChatManager.choice1.onClick.AddListener(T0_IncConfront);
        } else {
            PlayerChoices.chatProgress = 1;
        }
    }

    private void T0_IncConfront() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: Why don't you have yours filled in?\n");

        StartCoroutine(T0_IncConReply());
    }

    private IEnumerator T0_IncConReply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f, 1.5f));

        ChatManager.updateChatRecords("\nReese: You never know who's reading your details.\n");
        PlayerChoices.friendPrivacy = true;
    }

   

    /***
    TIME 0: COMPLETE PROFILE
    ***/

    private void T0_StartComplete() {
        Debug.Log("complete script");
    }

}