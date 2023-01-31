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

    void OnEnable() {
        exclamationMark.gameObject.SetActive(false);
        CalcExclMark();
    }

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

    // only graphics; doesn't control whether the player can chat with Reese or not
    // default is they can always chat with Reese which is objectively bad
    private void CalcExclMark() {
        if (PlayerPrefs.GetInt("DayCount") == 0) {

            if (PlayerPrefs.GetInt("TimeCount") == 2 && PlayerChoices.chatProgress != 1) {
                // D0T2; friend req, if not finished chat
                exclamationMark.gameObject.SetActive(true);
            }

        } else if (PlayerPrefs.GetInt("DayCount") == 1) {
            
            if (PlayerPrefs.GetInt("TimeCount") == 0) {
                // privacy
                if (PlayerChoices.chatProgress != 1) {
                    exclamationMark.gameObject.SetActive(true);
                }

            } else if (PlayerPrefs.GetInt("TimeCount") == 1) {
                // groceries
                if (PlayerChoices.groceryDone && PlayerChoices.chatProgress != 1) {
                    exclamationMark.gameObject.SetActive(true);
                }

            } else if (PlayerPrefs.GetInt("TimeCount") == 2) {
                // plant
                if (PlayerChoices.plantType != "" && PlayerChoices.chatProgress != 1) {
                    exclamationMark.gameObject.SetActive(true);
                }
            }

        } else if (PlayerPrefs.GetInt("DayCount") == 2) {

        }
    }
}
