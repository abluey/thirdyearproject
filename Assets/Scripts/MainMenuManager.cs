using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    // [SerializeField] private Button startGameBtn;
    // [SerializeField] private string startSceneName;
    [SerializeField] private Button quitBtn;

    // Start is called before the first frame update
    void Start() {
        // startGameBtn.onClick.AddListener(StartGame);
        quitBtn.onClick.AddListener(QuitConfirm);
    }

    // Update is called once per frame
    // void Update() {
        
    // }

    // private void StartGame() {
    //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(startSceneName, LoadSceneMode.Additive);
    //     asyncLoad.completed += OnStartGame;
    // }

    // private void OnStartGame(AsyncOperation loadOperation) {
    //     _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    // }

    private void QuitConfirm() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Quit", LoadSceneMode.Additive);
    }
}
