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
        yesBtn.onClick.AddListener(quitYes);
        noBtn.onClick.AddListener(quitNo);
    }

    // Update is called once per frame
    // void Update() {
        
    // }

    private void quitYes() {
        Application.Quit();
    }

    private void quitNo() {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
