using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeechModalBack : MonoBehaviour
{   
    private int warning;

    [SerializeField] private TMPro.TMP_Text thought;

    void Start()
    {
        warning = ClockManager.clockBlock;
    }

    public void Back() {
        if (warning == 0) {
            _ = SceneManager.UnloadSceneAsync(gameObject.scene);
        }

        if (warning == 201) {
            thought.text = "I should set up my FriendMi profile first.";
        }

        if (warning == 202) {
            thought.text = "I should check my laptop notification.";
        }
    }
}
