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
        switch (warning) {
            case 100: thought.text = "I should set up my FriendMi profile first."; break;
            case 101: thought.text = "I should buy something from the grocery store first."; break;
            case 102: thought.text = "I should check my laptop notification."; break;
            case 202: thought.text = "I should introduce myself to my new friend on FriendMi."; break;
            case 302: thought.text = "I should finish my chat with Reese."; break;
            default:
                thought.text = "(You shouldn't be seeing this screen right now.)";
                break;
        }
    }

    public void Back() {
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
