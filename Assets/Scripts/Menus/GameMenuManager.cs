using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private Button saveBtn;

    void Start() {
        saveBtn.onClick.AddListener(Save);
    }

    private void Save() {
        PlayerPrefs.Save();
        PlayerChoices.ResetChoices();
        StartCoroutine(WaitLoad());
    }

    private IEnumerator WaitLoad() {
        yield return new WaitForSeconds(0.2f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("StartMenu", LoadSceneMode.Additive);
        asyncLoad.completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperation loadOperation) {
        // unloads GameMenu and also Room scenes
        for (int n = 0; n < SceneManager.sceneCount; ++n)
            {
                Scene scene = SceneManager.GetSceneAt(n);
                if (scene.name == "Room") {
                    _ = SceneManager.UnloadSceneAsync(scene.name);
                }
            }
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
