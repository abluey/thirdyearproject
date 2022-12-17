using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsManager : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text noFriendsText;

    [SerializeField] private Button newFriendRequest;
    [SerializeField] private TMPro.TMP_Text newFriendReqText;
    [SerializeField] private Button quitBtn;

    [SerializeField] private Canvas chatPage;

    void Start()
    {   
        chatPage.gameObject.SetActive(false);
        noFriendsText.gameObject.SetActive(false);
        newFriendRequest.gameObject.SetActive(false);
        newFriendReqText.gameObject.SetActive(false);

        // no friends at all for morning and afternoon of the first day
        if (PlayerPrefs.GetInt("DayCount") == 0 && PlayerPrefs.GetInt("TimeCount") < 2) {
            noFriendsText.gameObject.SetActive(true);
        }

        // new friend req on evening of the first day
        if (PlayerPrefs.GetInt("DayCount") == 0 && PlayerPrefs.GetInt("TimeCount") == 2) {
            newFriendRequest.gameObject.SetActive(true);
            newFriendReqText.gameObject.SetActive(true);
            newFriendRequest.onClick.AddListener(AcceptFriendReq);
        }
    }

    private void AcceptFriendReq() {
        PlayerChoices.acceptRequest = true;
        gameObject.SetActive(false);
        chatPage.gameObject.SetActive(true);
        quitBtn.gameObject.SetActive(false);    // controlled here because if chatMan controls it it'll just disappear
    }
}
