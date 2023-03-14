using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadGameManager : MonoBehaviour
{
    [SerializeField] private Button playBtn;

    public TMPro.TMP_Text Content;

    // Start is called before the first frame update
    void Start()
    {   
        // haven't touched the game; Day 0 Time 0
        if (PlayerPrefs.GetInt("DayCount") == 0 && PlayerPrefs.GetInt("TimeCount") == 0 && !PlayerPrefs.HasKey("Name")) {
            playBtn.interactable = false;
            Content.text = "No saved game found.";
        } else {
            playBtn.interactable = true;
            Content.text = PlayerPrefs.GetString("Name") + " - Day " + PlayerPrefs.GetInt("DayCount") + ", " + GetTimeName(PlayerPrefs.GetInt("TimeCount"));
            playBtn.onClick.AddListener(Play);
        }
    }

    // dupe from TimeTransition but I'm not about to drag the entire TT script as a game component for Loading a goddamn game
    private string GetTimeName(int time) {
        switch (time) {
            case 0: return "morning";
            case 1: return "afternoon";
            case 2: return "evening";
            default: return "ERROR: illegal time number";
        }
    }

    private void Play() {
        StartCoroutine(WaitLoad());
    }

    private IEnumerator WaitLoad() {
        yield return new WaitForSeconds(0.2f);
        Save.LoadData();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Room", LoadSceneMode.Additive);
        asyncLoad.completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperation loadOperation) {
        
        // unloading the menu since Load Game is a popup modal
        for (int n = 0; n < SceneManager.sceneCount; ++n)
            {
                Scene scene = SceneManager.GetSceneAt(n);
                if (scene.name == "StartMenu") {
                    _ = SceneManager.UnloadSceneAsync(scene.name);
                }
            }

        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
