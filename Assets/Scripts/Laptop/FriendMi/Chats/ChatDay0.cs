using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatDay0 : MonoBehaviour {
    // any way to not repeat ChatManager. every single time -v-

    // no way to access this script unless it's Day 0 Time 2
    void OnEnable() {
        if (!PlayerChoices.introducedYourself) {
            ChatManager.chatbox.text = "Day 0\nNo new messages today.";
            ChatManager.choice1.gameObject.SetActive(true);
            ChatManager.choice1text.text = "Hello, my name is " + PlayerPrefs.GetString("Name");

            ChatManager.choice1.onClick.AddListener(Intro);

        } else {
            ChatManager.chatbox.text = PlayerChoices.chatRecord;

            switch (PlayerChoices.chatProgress) {
                case 1: break;
                case 2: IntroReplyChoice(); break;
                case 3: Intro2ReplyChoice(); break;
                case 4: Intro3ReplyChoice(); break;
                default: Debug.Log("Something went wrong"); break;
            }
        }
    }
    
    /***
    INTRO
    ***/

    private void Intro() {
        ChatManager.ResetListeners();

        PlayerChoices.introducedYourself = true;
        ChatManager.chatbox.text = "Day 0\nYou: Hello, my name is " + PlayerPrefs.GetString("Name") + "\n";
        PlayerChoices.chatRecord = ChatManager.chatbox.text;
        
        ChatManager.ShowChoices(false);

        StartCoroutine(IntroReply());
    }

    private IEnumerator IntroReply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: Hi there! Nice to meet you.\n");
        PlayerChoices.chatProgress = 2;
        IntroReplyChoice();
    }

    // separation is necessary so the updateChatRecords doesn't repeat when loading back into the game from menu
    // Keep Reese's chats self-contained
    public void IntroReplyChoice() {
        // set up the reply choice
        ChatManager.choice1text.text = "Nice to meet you too.";

        ChatManager.choice1.onClick.AddListener(Intro2);
        ChatManager.choice1.gameObject.SetActive(true);
    }

    private void Intro2() {
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Nice to meet you too.\n");
        ChatManager.ShowChoices(false);

        StartCoroutine(Intro2Reply());
    }

    private IEnumerator Intro2Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));

        ChatManager.updateChatRecords("\nReese: Not many people use FriendMi now. Glad to see a new face!\n");
        PlayerChoices.chatProgress = 3;
        Intro2ReplyChoice();
    }

    public void Intro2ReplyChoice() {
        ChatManager.choice1text.text = "I'm looking forward to our chats :)";
        ChatManager.choice2text.text = "Oh, why?";

        ChatManager.choice1.onClick.AddListener(Intro2Reply1);
        ChatManager.choice2.onClick.AddListener(Intro2Reply2);
        ChatManager.ShowChoices(true);
    }

    private void Intro2Reply1() {
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: I'm looking forward to our chats :)\n");

        ChatManager.ShowChoices(false);

        StartCoroutine(IntroFinish());
    }

    private void Intro2Reply2() {
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Oh, why?\n");

        ChatManager.ShowChoices(false);

        StartCoroutine(Intro3Reply());
    }

    private IEnumerator Intro3Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(3.5f));

        ChatManager.updateChatRecords("\nReese: There's been an influx of bot accounts lately, so people have moved on.\n");
        PlayerChoices.chatProgress = 4;
        Intro3ReplyChoice();
    }

    public void Intro3ReplyChoice() {
        ChatManager.choice1text.text = "I see. Well, I look forward to our chats anyway.";

        ChatManager.choice1.onClick.AddListener(IntroFinish1);
        ChatManager.choice1.gameObject.SetActive(true);
    }

    private IEnumerator IntroFinish() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        yield return ChatManager.IsTyping(1.0f);

        ChatManager.updateChatRecords("\nReese: Of course! I'll see you around.");
        PlayerChoices.chatProgress = 1;
    }

    private void IntroFinish1() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: I... see. Well, I look forward to our chats anyway.\n");

        StartCoroutine(IntroFinish2());
    }

    private IEnumerator IntroFinish2() {
        yield return StartCoroutine(ChatManager.IsTyping(1.0f));
        ChatManager.updateChatRecords("\nReese: Same here! I'll see you around.");
        PlayerChoices.chatProgress = 1;
    }
}
