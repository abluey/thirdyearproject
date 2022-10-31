using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToMenuManager : MonoBehaviour
{
    [SerializeField] private Button saveBtn;
    [SerializeField] private Button menuBtn;
    [SerializeField] private Button closeBtn;

    public TMPro.TMP_Text savedText;

    void Start() {
        savedText.text = "";

        saveBtn.onClick.AddListener(Save);
        menuBtn.onClick.AddListener(Menu);
        closeBtn.onClick.AddListener(Close);
    }

    private void Menu() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        asyncLoad.completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperation loadOperation) {
        for (int n = 0; n < SceneManager.sceneCount; ++n)
            {
                Scene scene = SceneManager.GetSceneAt(n);
                if (scene.name == "Kitchen" || scene.name == "Room") {
                    SceneManager.UnloadSceneAsync(scene.name);
                }
            }
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }

    private void Save() {
        // save
        PlayerPrefs.SetInt("DayCount", 1);
        PlayerPrefs.SetInt("TimeCount", 2);
        PlayerPrefs.Save();
        // show text saying game saved
        if (PlayerPrefs.HasKey("DayCount")) {
            StartCoroutine(ShowSavedNotif());
        }
    }

    private void Close() {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }

    IEnumerator ShowSavedNotif() {
        savedText.text = "Game saved.";
        yield return new WaitForSeconds (2.0f);
        savedText.text = "";
    }
}
