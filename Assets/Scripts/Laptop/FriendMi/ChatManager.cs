using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    // default message when first added day 0 time 2 (evening)

    // chatProgress saves just after updatechatbox with Reese's reply
    // the next step after that is ALWAYS presenting with new choices / finish
    // 0 means haven't started
    // 1 means completely finished

    [SerializeField] private Button homeBtn;
    [SerializeField] private Button quitApp;
    [SerializeField] private Button logBtn;

    [SerializeField] private Canvas homepage;
    [SerializeField] private Canvas logPage;

    [SerializeField] private TMPro.TMP_Text chatbox;
    [SerializeField] private Button choice1;
    [SerializeField] private Button choice2;

    [SerializeField] private TMPro.TMP_Text choice1text;
    [SerializeField] private TMPro.TMP_Text choice2text;
    [SerializeField] private TMPro.TMP_Text isTyping;

    void OnEnable() {
        choice1.onClick.RemoveAllListeners();
        choice2.onClick.RemoveAllListeners();
        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);
        StopAllCoroutines();
        CalcChatText();
    }

    void Start()
    {
        homeBtn.onClick.AddListener(Home);
        logBtn.onClick.AddListener(ChatLog);
        isTyping.gameObject.SetActive(false);
    }

    private void Home() {
        homepage.gameObject.SetActive(true);
        quitApp.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ChatLog() {
        logPage.gameObject.SetActive(true);
    }

    private void CalcChatText() {
        if (!PlayerChoices.introducedYourself) {
            chatbox.text = "Day 0\nNo new messages today.";
            choice1.gameObject.SetActive(true);
            choice1text.text = "Hello, my name is " + PlayerPrefs.GetString("Name") + "\n";

            choice1.onClick.AddListener(Intro);
        } else if (PlayerPrefs.GetInt("DayCount") == 0) {
            Debug.Log(PlayerChoices.chatProgress);
            chatbox.text = PlayerChoices.chatRecord;

            switch (PlayerChoices.chatProgress) {
                case 1: break;
                case 2: IntroReplyChoice(); break;
                case 3: Intro2ReplyChoice(); break;
                case 4: Intro3ReplyChoice(); break;
                default: Debug.Log("Something went wrong"); break;
            }
        } else {
            chatbox.text = "Day " + PlayerPrefs.GetInt("DayCount").ToString() + "\nNo new messages today.";
        }
    }

    private IEnumerator IsTyping(float num) {
        homeBtn.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        isTyping.gameObject.SetActive(true);
        yield return new WaitForSeconds(num);
        isTyping.gameObject.SetActive(false);
        homeBtn.gameObject.SetActive(true);
    }

    public void updateChatRecords(string text) {
        chatbox.text += text;
        PlayerChoices.chatRecord += text;
    }

    private void Intro() {
        // update chat box and chat record

        choice1.onClick.RemoveListener(Intro);
        PlayerChoices.introducedYourself = true;
        chatbox.text = "Day 0\nYou: Hello, my name is " + PlayerPrefs.GetString("Name") + "\n";
        PlayerChoices.chatRecord = chatbox.text;
        choice1.gameObject.SetActive(false);

        StartCoroutine(IntroReply());
    }

    private IEnumerator IntroReply() {
        // IsTyping; update chat box and chat record with reply

        yield return StartCoroutine(IsTyping(2.0f));
        updateChatRecords("\nReese: Hi there! Nice to meet you.\n");
        PlayerChoices.chatProgress = 2;
        IntroReplyChoice();
    }

    // separation is necessary so the updatechatrecord doesn't repeat when loading back into the game from menu
    // Keep Reese's chats self-contained
    private void IntroReplyChoice() {
        // set up the reply choice
        choice1text.text = "Nice to meet you too.";

        choice1.onClick.AddListener(Intro2);
        choice1.gameObject.SetActive(true);
    }

    private void Intro2() {
        choice1.onClick.RemoveListener(Intro2);

        updateChatRecords("\nYou: Nice to meet you too.\n");
        choice1.gameObject.SetActive(false);

        StartCoroutine(Intro2Reply());
    }

    private IEnumerator Intro2Reply() {
        yield return StartCoroutine(IsTyping(2.0f));

        updateChatRecords("\nReese: Not many people use FriendMi now. Glad to see a new face!\n");
        PlayerChoices.chatProgress = 3;
        Intro2ReplyChoice();
    }

    private void Intro2ReplyChoice() {
        choice1text.text = "I'm looking forward to our chats :)";
        choice2text.text = "Oh, why?";

        choice1.onClick.AddListener(Intro2Reply1);
        choice1.gameObject.SetActive(true);

        choice2.onClick.AddListener(Intro2Reply2);
        choice2.gameObject.SetActive(true);
    }

    private void Intro2Reply1() {
        choice1.onClick.RemoveListener(Intro2Reply1);
        choice2.onClick.RemoveListener(Intro2Reply2);

        updateChatRecords("\nYou: I'm looking forward to our chats :)\n");

        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);

        StartCoroutine(IntroFinish());
    }

    private void Intro2Reply2() {
        choice1.onClick.RemoveListener(Intro2Reply1);
        choice2.onClick.RemoveListener(Intro2Reply2);

        updateChatRecords("\nYou: Oh, why?\n");

        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);

        StartCoroutine(Intro3Reply());
    }

    private IEnumerator Intro3Reply() {
        yield return StartCoroutine(IsTyping(3.5f));

        updateChatRecords("\nReese: I'm surprised you haven't heard. There's rumors of a security breach. But honestly? Everything seems fine to me.\n");
        PlayerChoices.chatProgress = 4;
        Intro3ReplyChoice();
    }

    private void Intro3ReplyChoice() {
        choice1text.text = "I... see. Well, I look forward to our chats anyway.";

        choice1.onClick.AddListener(IntroFinish1);
        choice1.gameObject.SetActive(true);
    }

    private IEnumerator IntroFinish() {
        choice1.onClick.RemoveAllListeners();
        choice2.onClick.RemoveAllListeners();
        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);

        yield return IsTyping(1.0f);

        updateChatRecords("\nReese: Of course! I'll see you around.");
        PlayerChoices.chatProgress = 1;
    }

    private void IntroFinish1() {
        choice1.onClick.RemoveAllListeners();
        choice2.onClick.RemoveAllListeners();
        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);

        updateChatRecords("\nYou: I... see. Well, I look forward to our chats anyway.\n");

        StartCoroutine(IntroFinish2());
    }

    private IEnumerator IntroFinish2() {
        yield return StartCoroutine(IsTyping(1.0f));
        updateChatRecords("\nReese: Same here! I'll see you around.");
        PlayerChoices.chatProgress = 1;
    }

}
