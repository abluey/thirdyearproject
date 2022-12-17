using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FMManager : MonoBehaviour
{
    [SerializeField] private Button friendsBtn;
    [SerializeField] private Button profileBtn;
    [SerializeField] private Button aboutBtn;

    [SerializeField] private Canvas homepage;
    [SerializeField] private Canvas friends;
    [SerializeField] private Canvas profile;
    [SerializeField] private Canvas about;
    [SerializeField] private Canvas chat;
    [SerializeField] private Canvas chatLog;

    [SerializeField] private Button homeBtn;
    private Canvas lastVisited;

    void Start()
    {
        friendsBtn.onClick.AddListener( delegate { LoadCanvas(friends); });
        profileBtn.onClick.AddListener(delegate { LoadCanvas(profile);});
        aboutBtn.onClick.AddListener(delegate { LoadCanvas(about);});
        homeBtn.onClick.AddListener(Home);
        
        friends.gameObject.SetActive(false);
        about.gameObject.SetActive(false);
        chat.gameObject.SetActive(false);
        chatLog.gameObject.SetActive(false);
        
        homeBtn.gameObject.SetActive(false);

        if (!PlayerPrefs.HasKey("Name")) {
            LoadCanvas(profile);
        } else {
            profile.gameObject.SetActive(false);
        }
    }

    private void LoadCanvas(Canvas canvas) {
        canvas.gameObject.SetActive(true);
        homepage.gameObject.SetActive(false);
        homeBtn.gameObject.SetActive(true);
        lastVisited = canvas;
    }

    private void Home() {
        homepage.gameObject.SetActive(true);
        lastVisited.gameObject.SetActive(false);
        homeBtn.gameObject.SetActive(false);
    }
}
