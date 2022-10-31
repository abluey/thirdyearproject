using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadGameManager : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button backBtn;

    public TMPro.TMP_Text Content;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("DayCount")) {
            playBtn.interactable = true;
            Content.text = "Saved game found - Day " + PlayerPrefs.GetInt("DayCount");
            playBtn.onClick.AddListener(Play);
        } else {
            playBtn.interactable = false;
            Content.text = "No saved game found.";
        }
        backBtn.onClick.AddListener(Back);
    }

    private void Play() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Room", LoadSceneMode.Additive);
        asyncLoad.completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperation loadOperation) {
        for (int n = 0; n < SceneManager.sceneCount; ++n)
            {
                Scene scene = SceneManager.GetSceneAt(n);
                if (scene.name == "MainMenu") {
                    SceneManager.UnloadSceneAsync(scene.name);
                }
            }
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }

    private void Back() {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
