using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour
{
    [SerializeField] private Button clockBtn;

    public static int clockBlock;  // format: blocknum/day/time
    
    void Start()
    {
        clockBlock = 0;
        clockBtn.onClick.AddListener(AdvanceTime);
    }

    private void AdvanceTime() {

        // no profile set on first day
        if (!PlayerPrefs.HasKey("Name")) {
            clockBlock = 100;
        }

        // shopping not done on afternoon first day
        if (PlayerPrefs.GetInt("DayCount") == 0 && PlayerPrefs.GetInt("TimeCount") == 1 && !PlayerChoices.groceryDone) {
            clockBlock = 101;
        }

        // FriendMi stuff not done
        if (PlayerPrefs.GetInt("DayCount") == 0 && PlayerPrefs.GetInt("TimeCount") == 2 &&
          (!PlayerChoices.acceptRequest || !PlayerChoices.introducedYourself || PlayerChoices.chatProgress != 1)) {
            
            if (!PlayerChoices.acceptRequest) clockBlock = 102;
            else if (!PlayerChoices.introducedYourself) clockBlock = 202;
            else if (PlayerChoices.chatProgress != 1) clockBlock = 302;
        }

        // everything ok
        if (clockBlock == 0) {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TimeConfirm", LoadSceneMode.Additive);
        } else {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SpeechModal", LoadSceneMode.Additive);
        }
        

        // else if (PlayerPrefs.GetInt("CompletedTask") == 1) {
        //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TimeTransition", LoadSceneMode.Additive);
        //     asyncLoad.completed += OnLoadComplete;
        // }
    }

    // private void OnLoadComplete(AsyncOperation loadOperation) {
    //     _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    // }
}
