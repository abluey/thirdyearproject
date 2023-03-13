using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LaptopManager : MonoBehaviour
{
    [SerializeField] private Button quitBtn;

    [SerializeField] private Button friendBtn;
    [SerializeField] private Button plantBtn;
    [SerializeField] private Button birthdayBtn;
    [SerializeField] private Button shopBtn;

    [SerializeField] private Image exclMark;

    void OnEnable() {
        if (PlayerChoices.chatProgress == 1) {
            exclMark.gameObject.SetActive(false);
        } else if (BedroomManager.markActive) {
            exclMark.gameObject.SetActive(true);
        } else {
            exclMark.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        quitBtn.onClick.AddListener(delegate { LoadScene("Room"); });

        friendBtn.onClick.AddListener(delegate { LoadScene("FriendMi"); });
        plantBtn.onClick.AddListener(delegate { LoadScene("PlantSite"); });
        birthdayBtn.onClick.AddListener(delegate { LoadScene("BirthdaySite"); });
        shopBtn.onClick.AddListener(delegate { LoadScene("Groceries"); });
    }

    private void LoadScene(string sceneName) {
        StartCoroutine(WaitLoad(sceneName));
    }

    private IEnumerator WaitLoad(string sceneName) {
        yield return new WaitForSeconds(0.2f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncLoad.completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperation loadOperation) {
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
