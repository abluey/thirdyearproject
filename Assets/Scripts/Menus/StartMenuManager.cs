using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button hasPlayedStartBtn;
    [SerializeField] private Button loadBtn;
    [SerializeField] private Button creditsBtn;
    [SerializeField] private Button quitBtn;

    [SerializeField] private Canvas load;
    [SerializeField] private Canvas credits;
    [SerializeField] private Canvas startConfirm;

    private bool hasPlayed = false;

    void Start() {
        load.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        startConfirm.gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("DayCount") == 0 && PlayerPrefs.GetInt("TimeCount") == 0 && !PlayerPrefs.HasKey("Name")) {
            loadBtn.interactable = false;
            hasPlayed = false;
        } else { 
            hasPlayed = true;
            loadBtn.onClick.AddListener(Load);
        }
        
        if (hasPlayed) {
            hasPlayedStartBtn.gameObject.SetActive(true);
            startBtn.gameObject.SetActive(false);

            hasPlayedStartBtn.onClick.AddListener(StartGame);
        } else {
            // the set actives may be redundant
            hasPlayedStartBtn.gameObject.SetActive(false);
            startBtn.gameObject.SetActive(true);

            startBtn.onClick.AddListener(Reset);
        }
        
        creditsBtn.onClick.AddListener(Credits);

        quitBtn.onClick.AddListener(delegate { PopScene("Quit"); });
    }

    private void Reset() {
        PlayerPrefs.DeleteAll();
        Save.DeleteAllData();
    }

    private void StartGame() {
        startConfirm.gameObject.SetActive(true);
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
}
