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

    // can only access this page and script if the frd req has been accepted

    private static Button homeBtn;
    [SerializeField] private Button nonstaticHomeBtn;
    [SerializeField] private Button quitApp;
    [SerializeField] private Button logBtn;

    [SerializeField] private Button friendProfile;
    [SerializeField] private Canvas friendProfilePage;

    [SerializeField] private Canvas homepage;
    [SerializeField] private Canvas logPage;

    public static Button choice1;
    public static Button choice2;
    public static TMPro.TMP_Text choice1text;
    public static TMPro.TMP_Text choice2text;

    public static TMPro.TMP_Text chatbox;
    private static TMPro.TMP_Text isTypingText;

    private ChatDay0 day0;
    private ChatDay1 day1;

    void Awake() {
        homeBtn = nonstaticHomeBtn;
        isTypingText = gameObject.transform.Find("FriendIsTyping").GetComponent<TMPro.TMP_Text>();
        chatbox = gameObject.transform.Find("FriendChat/Viewport/Content/Chatbox").GetComponent<TMPro.TMP_Text>();
        choice1 = gameObject.transform.Find("Choice 1").GetComponent<Button>();
        choice2 = gameObject.transform.Find("Choice 2").GetComponent<Button>();

        choice1 = gameObject.transform.Find("Choice 1").GetComponent<Button>();
        choice2 = gameObject.transform.Find("Choice 2").GetComponent<Button>();

        choice1text = gameObject.transform.Find("Choice 1/Choice1Text").GetComponent<TMPro.TMP_Text>();
        choice2text = gameObject.transform.Find("Choice 2/Choice2Text").GetComponent<TMPro.TMP_Text>();

        day0 = gameObject.GetComponent<ChatDay0>();
        day1 = gameObject.GetComponent<ChatDay1>(); 
    }

    void OnEnable() {
        ResetListeners();
        ShowChoices(false);
        
        day0.enabled = false;
        day1.enabled = false;

        StopAllCoroutines();
        CalcChatText();
    }

    void Start()
    {
        homeBtn.onClick.AddListener(Home);
        logBtn.onClick.AddListener(ChatLog);
        isTypingText.gameObject.SetActive(false);
        
        friendProfile.onClick.AddListener(FriendProfile);
    }

    private void Home() {
        homepage.gameObject.SetActive(true);
        quitApp.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ChatLog() {
        logPage.gameObject.SetActive(true);
    }

    private void FriendProfile() {
        friendProfilePage.gameObject.SetActive(true);
        PlayerChoices.checkedFriendProfile = true;
    }

    private void CalcChatText() {
        chatbox.text = "No messages";

        // unaccessible unless accepted frd req on D0 T2 (controlled in FriendsManager)
        // so, like, this is safe
        if (PlayerPrefs.GetInt("DayCount") == 0) {    
            day0.enabled = true;
        }

        else if (PlayerPrefs.GetInt("DayCount") == 1) {
            if (PlayerPrefs.GetInt("TimeCount") == 0) {
                day1.enabled = true;
            }
        }

    }

    public static IEnumerator IsTyping(float num, float num2 = 0.5f) {
        homeBtn.gameObject.SetActive(false);
        yield return new WaitForSeconds(num2);
        isTypingText.gameObject.SetActive(true);
        yield return new WaitForSeconds(num);
        isTypingText.gameObject.SetActive(false);
        homeBtn.gameObject.SetActive(true);
    }

    // ELEPHANT: would string text work if you just put "ChatManager.choice1text.text"?
    public static void updateChatRecords(string text) {
        chatbox.text += text;
        PlayerChoices.chatRecord += text;
    }

    public static void ResetListeners() {
        choice1.onClick.RemoveAllListeners();
        choice2.onClick.RemoveAllListeners();
    }

    public static void ShowChoices(bool active) {
        if (active) {
            choice1.gameObject.SetActive(true);
            choice2.gameObject.SetActive(true);
        } else {
            choice1.gameObject.SetActive(false);
            choice2.gameObject.SetActive(false);
        }
        
    }

}
