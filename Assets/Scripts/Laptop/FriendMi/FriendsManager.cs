using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsManager : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text noFriendsText;

    [SerializeField] private Button newFriendRequest;
    [SerializeField] private Canvas chatPage;

    void Start()
    {   
        chatPage.gameObject.SetActive(false);
        noFriendsText.gameObject.SetActive(false);
        newFriendRequest.gameObject.SetActive(false);

        // no friends at all for morning and afternoon of the first day
        if (PlayerPrefs.GetInt("DayCount") == 0 && PlayerPrefs.GetInt("TimeCount") < 2) {
            noFriendsText.gameObject.SetActive(true);
        }

        // new friend req on evening of the first day
        if (PlayerPrefs.GetInt("DayCount") == 0 && PlayerPrefs.GetInt("TimeCount") == 2) {
            newFriendRequest.gameObject.SetActive(true);
            newFriendRequest.onClick.AddListener(AcceptFriendReq);
        }
    }

    private void AcceptFriendReq() {
        gameObject.SetActive(false);
        chatPage.gameObject.SetActive(true);
    }
}
