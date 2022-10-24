using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private Button saveBtn;
    [SerializeField] private Button quitBtn;

    public TMPro.TMP_Text savedText;
    
    
    // Start is called before the first frame update
    void Start() {
        savedText.text = "";
        saveBtn.onClick.AddListener(Save);
        quitBtn.onClick.AddListener(QuitConfirm);
    }

    private void Save() {
        // save
        PlayerPrefs.SetInt("DayCount", 3);
        PlayerPrefs.Save();
        // show text saying game saved
        if (PlayerPrefs.HasKey("DayCount")) {
            StartCoroutine(ShowSavedNotif());
        }
    }

    private void QuitConfirm() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Quit", LoadSceneMode.Additive);
    }

    IEnumerator ShowSavedNotif() {
        savedText.text = "Game saved.";
        yield return new WaitForSeconds (1.0f);
        savedText.text = "";
    }
}
