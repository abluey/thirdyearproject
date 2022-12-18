using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitManager : MonoBehaviour
{
    [SerializeField] private Button yesBtn;
    [SerializeField] private Button noBtn;

    void Start() {
        yesBtn.onClick.AddListener(QuitYes);
        noBtn.onClick.AddListener(QuitNo);
    }

    private void QuitYes() {
        Save.SaveData();
        Application.Quit();
    }

    private void QuitNo() {
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
