using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HasPlayedStart : MonoBehaviour
{
    [SerializeField] private Button play;

    void Start() {
        play.onClick.AddListener(PlayStart);
    }

    private void PlayStart() {
        PlayerPrefs.DeleteAll();
        Save.DeleteAllData();
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