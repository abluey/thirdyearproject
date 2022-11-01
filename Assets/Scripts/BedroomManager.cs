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

    void Start()
    {
        list.gameObject.SetActive(false);
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
}
