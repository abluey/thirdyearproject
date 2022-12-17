using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    // default message when first added day 0 time 2 (evening)
    // 

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

    void Start()
    {
        homeBtn.onClick.AddListener(Home);
        logBtn.onClick.AddListener(ChatLog);
        CalcChatText();
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
        if (PlayerChoices.acceptRequest && !PlayerChoices.introducedYourself) {
            chatbox.text = "No messages yet.";
            choice2.gameObject.SetActive(false);
            choice1text.text = "Hello, my name is " + PlayerPrefs.GetString("Name") + "\n";

            choice1.onClick.AddListener(Introduction);
        }
    }

    private void Introduction() {
        PlayerChoices.introducedYourself = true;
        chatbox.text = "You: Hello, my name is " + PlayerPrefs.GetString("Name") + "\n";
        PlayerChoices.chatRecord = "Day 0: \n" + chatbox.text;
    }
}
