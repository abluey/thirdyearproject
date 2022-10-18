using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button quitBtn;

    // Start is called before the first frame update
    void Start() {
        settingsBtn.onClick.AddListener(Settings);
        quitBtn.onClick.AddListener(QuitConfirm);
    }

    // Update is called once per frame
    // void Update() {
        
    // }

    private void Settings() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Settings", LoadSceneMode.Additive);
    }

    private void QuitConfirm() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Quit", LoadSceneMode.Additive);
    }
}
