using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGameManager : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button backBtn;

    public TMPro.TMP_Text Content;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("DayCount")) {
            playBtn.interactable = true;
            Content.text = "Saved game found - Day " + PlayerPrefs.GetInt("DayCount");
        } else {
            playBtn.interactable = false;
            Content.text = "No saved game found.";
        }
        backBtn.onClick.AddListener(Back);
    }

    private void Back() {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
