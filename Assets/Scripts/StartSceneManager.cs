using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private Button quitBtn;
    // [SerializeField] private Button backBtn;
    
    // Start is called before the first frame update
    void Start() {
        quitBtn.onClick.AddListener(QuitConfirm);
        // backBtn.onClick.AddListener(BackToMenu);
    }

    // Update is called once per frame
    // void Update() {
        
    // }

    private void QuitConfirm() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Quit", LoadSceneMode.Additive);
    }

    // private void BackToMenu() {
    //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
    //     asyncLoad.completed += OnSceneLoaded;
    // }

    // private void OnSceneLoaded(AsyncOperation loadOperation) {
    //     _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    // }
}
