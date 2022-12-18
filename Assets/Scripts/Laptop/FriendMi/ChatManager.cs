using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    // default message when first added day 0 time 2 (evening)

    // chatProgress only updates when player clicks button

    // chatProgress format is step/day/time
    // 0 means haven't started
    // 1 means completely finished
    // For day 0 time 2: Intro num + abc's; a = 1, b = 2, c = 3 (Example: Intro2b = 22)

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

            choice1.onClick.AddListener(Intro1);
        } else if (PlayerPrefs.GetInt("DayCount") == 0) {
            Debug.Log(PlayerChoices.chatProgress);
            chatbox.text = PlayerChoices.chatRecord;

            switch (PlayerChoices.chatProgress) {
                case 1: break;
                case 102: Intro1ab(); break;
                case 202: Intro2ab(); break;
                case 3102: Intro4(); break;
                case 3202: Intro3ca(); break;
                case 4102: Intro4b(); break;
                default: Debug.Log("Something went wrong"); break;
            }
        } else {
            chatbox.text = "Day " + PlayerPrefs.GetInt("DayCount").ToString() + "\nNo new messages today.";
        }
    }

    private IEnumerator IsTyping(float num) {
        yield return new WaitForSeconds(0.5f);
        isTyping.gameObject.SetActive(true);
        yield return new WaitForSeconds(num);
        isTyping.gameObject.SetActive(false);
    }

    private void updateChatRecords(string text) {
        chatbox.text += text;
        PlayerChoices.chatRecord += text;
    }

    private void Intro1() {
        // display chat text
        // update chat record

        choice1.onClick.RemoveListener(Intro1);
        PlayerChoices.introducedYourself = true;
        chatbox.text = "Day 0\nYou: Hello, my name is " + PlayerPrefs.GetString("Name") + "\n";
        PlayerChoices.chatRecord = chatbox.text;
        choice1.gameObject.SetActive(false);

        StartCoroutine(Intro1a());
    }

    private IEnumerator Intro1a() {
        // "istyping"
        // display chat reply
        // set up reply choices

        yield return StartCoroutine(IsTyping(2.0f));
        updateChatRecords("\nReese: Hi there! Nice to meet you.\n");
        PlayerChoices.chatProgress = 102;
        Intro1ab();
    }

    // separation is necessary so the updatechatrecord doesn't repeat when loading back into the game from menu
    private void Intro1ab() {
        choice1text.text = "Nice to meet you too.";

        choice1.onClick.AddListener(Intro2);
        choice1.gameObject.SetActive(true);
    }

    private void Intro2() {
        choice1.onClick.RemoveListener(Intro2);

        updateChatRecords("\nYou: Nice to meet you too.\n");
        choice1.gameObject.SetActive(false);

        StartCoroutine(Intro2a());
    }

    private IEnumerator Intro2a() {
        yield return StartCoroutine(IsTyping(2.0f));

        updateChatRecords("\nReese: Not many people use FriendMi now. Glad to see a new face!\n");
        PlayerChoices.chatProgress = 202;
        Intro2ab();
    }

    private void Intro2ab() {
        choice1text.text = "Looking forward to our chats :)";
        choice2text.text = "Oh, why?";

        choice1.onClick.AddListener(Intro3a);
        choice1.gameObject.SetActive(true);

        choice2.onClick.AddListener(Intro3b);
        choice2.gameObject.SetActive(true);
    }

    private void Intro3a() {
        choice1.onClick.RemoveListener(Intro3a);
        choice2.onClick.RemoveListener(Intro3b);

        updateChatRecords("\nYou: Looking forward to our chats :)\n");

        // if the player doesn't wait for Intro4 then the dialogue is lost (sucks to suck)
        // this is to avoid the double-chat problem when loading back into game
        PlayerChoices.chatProgress = 1;

        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);

        StartCoroutine(Intro4());
    }

    private void Intro3b() {
        choice1.onClick.RemoveListener(Intro3a);
        choice2.onClick.RemoveListener(Intro3b);

        updateChatRecords("\nYou: Oh, why?\n");

        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);

        StartCoroutine(Intro3c());
    }

    private IEnumerator Intro3c() {
        yield return StartCoroutine(IsTyping(3.5f));

        updateChatRecords("\nReese: I'm surprised you haven't heard. There's rumors of a security breach. But honestly? Everything seems fine to me.\n");
        PlayerChoices.chatProgress = 3202;
        Intro3ca();
    }

    private void Intro3ca() {
        choice1text.text = "I... see. Well, I look forward to our chats anyway.";

        choice1.onClick.AddListener(Intro4a);
        choice1.gameObject.SetActive(true);
    }

    private IEnumerator Intro4() {
        choice1.onClick.RemoveAllListeners();
        choice2.onClick.RemoveAllListeners();
        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);

        yield return IsTyping(1.0f);

        updateChatRecords("\nReese: Of course! I'll see you around.");
    }

    private void Intro4a() {
        choice1.onClick.RemoveAllListeners();
        choice2.onClick.RemoveAllListeners();
        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);
        PlayerChoices.chatProgress = 4102;

        updateChatRecords("\nYou: I... see. Well, I look forward to our chats anyway.\n");

        StartCoroutine(Intro4b());
    }

    private IEnumerator Intro4b() {
        yield return StartCoroutine(IsTyping(1.0f));
        updateChatRecords("\nReese: Same here! I'll see you around.");
        PlayerChoices.chatProgress = 1;
    }

}
