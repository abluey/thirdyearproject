using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsManager : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text noFriendsText;

    [SerializeField] private Button newFriendBtn;
    [SerializeField] private GameObject newFriendReq;

    [SerializeField] private GameObject chatWithFriend;
    [SerializeField] private Button chatBtn;

    [SerializeField] private Button quitBtn;

    [SerializeField] private Canvas chatPage;

    void OnEnable() {
        newFriendReq.SetActive(false);
        chatWithFriend.SetActive(false);

        if (PlayerChoices.acceptRequest) {
            chatWithFriend.SetActive(true);
            chatBtn.onClick.AddListener(Chat);
        }

        // new friend req on evening of the first day
        else if (PlayerPrefs.GetInt("DayCount") == 0 && PlayerPrefs.GetInt("TimeCount") == 2) {
            newFriendReq.SetActive(true);
            newFriendBtn.onClick.AddListener(AcceptFriendReq);
        }
    }

    void Start()
    {   
        chatPage.gameObject.SetActive(false);
        noFriendsText.gameObject.SetActive(false);
        
        // no friends at all for morning and afternoon of the first day
        if (PlayerPrefs.GetInt("DayCount") == 0 && PlayerPrefs.GetInt("TimeCount") < 2) {
            noFriendsText.gameObject.SetActive(true);
        }
    }

    private void AcceptFriendReq() {
        PlayerChoices.acceptRequest = true;
        gameObject.SetActive(false);
        chatPage.gameObject.SetActive(true);
        quitBtn.gameObject.SetActive(false);    // controlled here because if chatMan controls it it'll just disappear
    }

    private void Chat() {
        gameObject.SetActive(false);
        chatPage.gameObject.SetActive(true);
        quitBtn.gameObject.SetActive(false);
    }
}
