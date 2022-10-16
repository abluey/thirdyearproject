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

    // Update is called once per frame
    // void Update() {
        
    // }

    private void QuitYes() {
        Application.Quit();
    }

    private void QuitNo() {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
