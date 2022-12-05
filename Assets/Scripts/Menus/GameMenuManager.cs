using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private Button saveBtn;
    [SerializeField] private Button menuBtn;

    public TMPro.TMP_Text savedText;

    void Start() {
        savedText.text = "";

        saveBtn.onClick.AddListener(Save);
        menuBtn.onClick.AddListener(Menu);
    }

    private void Menu() {
        PlayerChoices.ResetChoices();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("StartMenu", LoadSceneMode.Additive);
        asyncLoad.completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperation loadOperation) {
        for (int n = 0; n < SceneManager.sceneCount; ++n)
            {
                Scene scene = SceneManager.GetSceneAt(n);
                if (scene.name == "Kitchen" || scene.name == "Room") {
                    _ = SceneManager.UnloadSceneAsync(scene.name);
                }
            }
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }

    private void Save() {
        Debug.Log("Day: " + PlayerPrefs.GetInt("DayCount") + " Time: " + PlayerPrefs.GetInt("TimeCount"));
        PlayerPrefs.Save();
        StartCoroutine(ShowSavedNotif());
    }

    IEnumerator ShowSavedNotif() {
        savedText.text = "Game saved.";
        yield return new WaitForSeconds (2.0f);
        savedText.text = "";
    }
}
