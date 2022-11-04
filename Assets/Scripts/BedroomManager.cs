using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BedroomManager : MonoBehaviour
{
    [SerializeField] private Button laptopBtn;
    [SerializeField] private Button todoListBtn;
    [SerializeField] private Button cheatBtn;

    [SerializeField] private Canvas list;

    void Start()
    {
        list.gameObject.SetActive(false);

        laptopBtn.onClick.AddListener(OpenLaptop);
        todoListBtn.onClick.AddListener(ToDoList);

        cheatBtn.onClick.AddListener(Cheat);
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

    private void Cheat() {
        int time = PlayerPrefs.GetInt("TimeCount");
        int day = PlayerPrefs.GetInt("DayCount");

        if (time < 2) {
            PlayerPrefs.SetInt("TimeCount", time + 1);
        } else {
            PlayerPrefs.SetInt("TimeCount", 0);
            PlayerPrefs.SetInt("DayCount", day + 1);
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
