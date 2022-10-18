using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private Button quitBtn;
    
    // Start is called before the first frame update
    void Start() {
        quitBtn.onClick.AddListener(QuitConfirm);
    }

    // Update is called once per frame
    // void Update() {
        
    // }

    private void QuitConfirm() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Quit", LoadSceneMode.Additive);
    }
}
