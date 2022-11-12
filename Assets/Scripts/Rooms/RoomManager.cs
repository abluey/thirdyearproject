using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private Button menuBtn;
    [SerializeField] private Button quitBtn;

    public Image background;
    public Color morning;
    public Color afternoon;
    public Color evening;
    public TMPro.TMP_Text day;
    
    void Start() {
        // setting background tint
        switch (PlayerPrefs.GetInt("TimeCount")) {
            case 0: background.color = morning; break;
            case 1: background.color = afternoon; break;
            case 2: background.color = evening; break;
        }

        day.text = "Day " + PlayerPrefs.GetInt("DayCount");

        menuBtn.onClick.AddListener( delegate { PopModal("GameMenu"); });
        quitBtn.onClick.AddListener( delegate { PopModal("Quit"); });;
    }

    private void PopModal(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
