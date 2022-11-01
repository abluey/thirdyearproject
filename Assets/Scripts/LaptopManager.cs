using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LaptopManager : MonoBehaviour
{
    [SerializeField] private Button quitBtn;

    // Start is called before the first frame update
    void Start()
    {
        quitBtn.onClick.AddListener(Close);
    }

    private void Close() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Room", LoadSceneMode.Additive);
        asyncLoad.completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperation loadOperation) {
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
