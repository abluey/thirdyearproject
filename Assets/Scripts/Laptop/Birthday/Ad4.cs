using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ad4 : MonoBehaviour
{
    [SerializeField] private Button xBtn;

    void Start()
    {
        xBtn.onClick.AddListener(ToMenu);
    }

    private void ToMenu() {
        Save.SaveData();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("StartMenu", LoadSceneMode.Additive);
        asyncLoad.completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperation loadOperation) {
        _ = SceneManager.UnloadSceneAsync("BirthdaySite");
    }

}
