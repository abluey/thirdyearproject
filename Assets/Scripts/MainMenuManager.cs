using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startGameBtn;
    [SerializeField] private string startSceneName;

    // Start is called before the first frame update
    void Start() {
        Debug.Log($"Button is null? {startGameBtn == null}");
        Debug.Log($"Button.OnClick is null? {startGameBtn.onClick == null}");
        startGameBtn.onClick.AddListener(startGame);
    }

    // Update is called once per frame
    // void Update() {
        
    // }

    private void startGame() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(startSceneName, LoadSceneMode.Additive);
        asyncLoad.completed += onStartGame;
    }

    private void onStartGame(AsyncOperation loadOperation) {
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
