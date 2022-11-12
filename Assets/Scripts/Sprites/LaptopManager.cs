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

    // Start is called before the first frame update
    void Start()
    {
        quitBtn.onClick.AddListener(delegate { LoadScene("Room"); });

        friendBtn.onClick.AddListener(delegate { LoadScene("FriendMi"); });
        plantBtn.onClick.AddListener(delegate { LoadScene("PlantSite"); });
        birthdayBtn.onClick.AddListener(delegate { LoadScene("BirthdaySite"); });
        shopBtn.onClick.AddListener(delegate { LoadScene("Groceries"); });
    }

    private void LoadScene(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncLoad.completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperation loadOperation) {
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
