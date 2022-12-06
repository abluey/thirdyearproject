using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BedroomManager : MonoBehaviour
{
    [SerializeField] private Button laptopBtn;
    [SerializeField] private Button todoListBtn;

    [SerializeField] private Canvas list;

    [SerializeField] private Image exclamationMark;

    void Start()
    {
        list.gameObject.SetActive(false);

        CalcExclMark();
        
        laptopBtn.onClick.AddListener(OpenLaptop);
        todoListBtn.onClick.AddListener(ToDoList);
    }

    private void OpenLaptop() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LaptopScreen", LoadSceneMode.Additive);
        asyncLoad.completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperation loadOperation) {
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }

    private void ToDoList() {
        list.gameObject.SetActive(true);
    }

    private void CalcExclMark() {
        // seeing if you need to put out the exclamation mark
        if (PlayerPrefs.GetInt("DayCount") == 0 && PlayerPrefs.GetInt("TimeCount") <= 1) {
            exclamationMark.gameObject.SetActive(false);
        } else {
            exclamationMark.gameObject.SetActive(true);
        }

        // for now, friend req will be first day evening
        // every time transition after that, there will be a new chat waiting
    }
}
