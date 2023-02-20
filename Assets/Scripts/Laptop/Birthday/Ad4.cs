using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ad4 : MonoBehaviour
{
    private string adName;
    [SerializeField] private Button xBtn;

    void OnEnable() {
        adName = BirthdayManager.adName;
        switch (adName) {
            case "ad1": break;
            case "ad2": break;
            case "ad3": break;
            case "ad5": break;
            default: Debug.Log("Something went wrong"); break;
        }
    }

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
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }

}
