using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private Button quitBtn;
    [SerializeField] private GameObject modalPanel;
    [SerializeField] private Button quitYesBtn;
    [SerializeField] private Button quitNoBtn;
    
    // Start is called before the first frame update
    void Start() {
        quitBtn.onClick.AddListener(quitConfirm);
        quitYesBtn.onClick.AddListener(quitYes);
        quitNoBtn.onClick.AddListener(quitNo);
        modalPanel.SetActive(false);
    }

    // Update is called once per frame
    // void Update() {
        
    // }

    private void quitConfirm() {
        modalPanel.SetActive(true);
    }

    private void quitYes() {
        Application.Quit();
    }
    
    private void quitNo() {
        modalPanel.SetActive(false);
    }
}
