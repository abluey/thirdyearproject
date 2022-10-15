using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startGameBtn;
    [SerializeField] private string startSceneName;
    [SerializeField] private Button quitBtn;

    // Start is called before the first frame update
    void Start() {
        startGameBtn.onClick.AddListener(startGame);
        quitBtn.onClick.AddListener(quitConfirm);
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

    private void quitConfirm() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Quit", LoadSceneMode.Additive);
    }
}
