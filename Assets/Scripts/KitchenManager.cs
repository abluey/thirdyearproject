using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class KitchenManager : MonoBehaviour
{
    [SerializeField] private Button menuBtn;
    [SerializeField] private Button quitBtn;

    public Image background;
    public Color morning;
    public Color afternoon;
    public Color evening;
    
    // Start is called before the first frame update
    void Start() {

        // setting background tint
        Debug.Log("Day: " + PlayerPrefs.GetInt("DayCount") + " Time: " + PlayerPrefs.GetInt("TimeCount"));
        switch (PlayerPrefs.GetInt("TimeCount")) {
            case 0: background.color = morning; break;
            case 1: background.color = afternoon; break;
            case 2: background.color = evening; break;
        }

        menuBtn.onClick.AddListener( delegate { PopModal("ToMenu"); });
        quitBtn.onClick.AddListener( delegate { PopModal("Quit"); });
    }

    private void PopModal(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

}
