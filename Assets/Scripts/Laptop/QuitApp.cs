using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitApp : MonoBehaviour
{
    [SerializeField] private Button quitApp;

    // Start is called before the first frame update
    void Start()
    {
        quitApp.onClick.AddListener(Quit);
    }

    private void Quit() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LaptopScreen", LoadSceneMode.Additive);
        asyncLoad.completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperation loadOperation) {
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
