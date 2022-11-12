using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button loadBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button creditsBtn;
    [SerializeField] private Button quitBtn;

    [SerializeField] private Canvas load;
    [SerializeField] private Canvas credits;
    [SerializeField] private Canvas settings;


// TODO with this: "Are you sure" when selecting New Game instead of Load Game (new modal needed)
    private bool hasPlayed = false;

    void Start() {
        load.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("DayCount") == 0 && PlayerPrefs.GetInt("TimeCount") == 0) {
            loadBtn.interactable = false;
            hasPlayed = false;
        } else { 
            hasPlayed = true;
            loadBtn.onClick.AddListener(Load);
        }
        
        startBtn.onClick.AddListener(Reset);

        creditsBtn.onClick.AddListener(Credits);
        settingsBtn.onClick.AddListener(Settings);

        quitBtn.onClick.AddListener(delegate { PopScene("Quit"); });
    }

    private void Reset() {
        PlayerPrefs.DeleteAll();
    }

    private void PopScene(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    private void Load() {
        load.gameObject.SetActive(true);
    }

    private void Credits() {
        credits.gameObject.SetActive(true);
    }

    private void Settings() {
        settings.gameObject.SetActive(true);
    }
}
