using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button loadBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button creditsBtn;
    [SerializeField] private Button quitBtn;

    // Start is called before the first frame update
    void Start() {
        if (PlayerPrefs.HasKey("DayCount")) {
            loadBtn.onClick.AddListener(delegate { PopModal("LoadGame"); });
        } else {
            loadBtn.interactable = false;
        }
        
        startBtn.onClick.AddListener(Reset);

        creditsBtn.onClick.AddListener(delegate { PopModal("Credits"); });
        settingsBtn.onClick.AddListener(delegate { PopModal("Settings"); });
        quitBtn.onClick.AddListener(delegate { PopModal("Quit"); });
    }

    private void Reset() {
        PlayerPrefs.DeleteAll();
    }

    private void PopModal(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
