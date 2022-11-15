using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsManager : MonoBehaviour
{
    [SerializeField] private Button homeBtn;

    [SerializeField] private Canvas homepage;

    [SerializeField] private TMPro.TMP_Text noFriendsText;

    void Start()
    {
        homeBtn.onClick.AddListener(Homepage);
        
        // if a friend is gotten
        if (PlayerPrefs.GetInt("HasFriend") == 1) {
            noFriendsText.gameObject.SetActive(true);
        } else {
            noFriendsText.gameObject.SetActive(false);
        }
    }

    private void Homepage() {
        homepage.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
