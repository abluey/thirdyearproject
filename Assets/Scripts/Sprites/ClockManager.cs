using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour
{
    [SerializeField] private Button clockBtn;
    
    // Start is called before the first frame update
    void Start()
    {
        clockBtn.onClick.AddListener(AdvanceTime);
    }

    private void AdvanceTime() {
        if (PlayerPrefs.GetInt("CompletedTask") == 0) {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TimeConfirm", LoadSceneMode.Additive);
        }
        else if (PlayerPrefs.GetInt("CompletedTask") == 1) {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TimeTransition", LoadSceneMode.Additive);
            asyncLoad.completed += OnLoadComplete;
        }
    }

    private void OnLoadComplete(AsyncOperation loadOperation) {
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
