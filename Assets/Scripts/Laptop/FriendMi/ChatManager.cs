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

    [SerializeField] private Canvas homepage;

    void Start()
    {
        homeBtn.onClick.AddListener(Home);
    }

    private void Home() {
        homepage.gameObject.SetActive(true);
        quitApp.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
