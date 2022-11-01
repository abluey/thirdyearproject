using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitManager : MonoBehaviour
{
    [SerializeField] private Button yesBtn;
    [SerializeField] private Button noBtn;

    // Start is called before the first frame update
    void Start() {
        yesBtn.onClick.AddListener(QuitYes);
        noBtn.onClick.AddListener(QuitNo);
    }

    private void QuitYes() {
        Application.Quit();
    }

    private void QuitNo() {
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
