using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button loadBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button creditsBtn;
    [SerializeField] private Button quitBtn;


// TODO with this: "Are you sure" when selecting New Game instead of Load Game (new modal needed)
    private bool hasPlayed = false;

    void Start() {

        if (PlayerPrefs.GetInt("DayCount") == 0 && PlayerPrefs.GetInt("TimeCount") == 0) {
            loadBtn.interactable = false;
            hasPlayed = false;
        } else { 
            loadBtn.onClick.AddListener(delegate { PopModal("LoadGame"); });
            hasPlayed = true;
        }
        
        startBtn.onClick.AddListener(Reset);

        creditsBtn.onClick.AddListener(delegate { PopModal("Credits"); });
        settingsBtn.onClick.AddListener(delegate { PopModal("Settings"); });
        quitBtn.onClick.AddListener(delegate { PopModal("Quit"); });
    }

    private void Reset() {
        PlayerPrefs.DeleteAll();
    }

    private void PopModal(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
