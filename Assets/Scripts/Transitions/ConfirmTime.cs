using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfirmTime : MonoBehaviour
{
    [SerializeField] private Button yesBtn;

    void Start()
    {
        yesBtn.onClick.AddListener(Confirm);
    }

    private void Confirm() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TimeTransition", LoadSceneMode.Additive);
        asyncLoad.completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperation loadOperation) {
        
        // unloading the room
        for (int n = 0; n < SceneManager.sceneCount; ++n)
            {
                Scene scene = SceneManager.GetSceneAt(n);
                if (scene.name == "Room" || scene.name == "Kitchen") {
                    _ = SceneManager.UnloadSceneAsync(scene.name);
                }
            }

        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
