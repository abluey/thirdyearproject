using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour
{
    [SerializeField] private Button clockBtn;

    public static int clockBlock;  // format: time/day/blocknum
    
    void Start()
    {
        clockBlock = 0;
        clockBtn.onClick.AddListener(AdvanceTime);
    }

    private void AdvanceTime() {
        if (PlayerPrefs.GetInt("DayCount") == 0 && PlayerPrefs.GetInt("TimeCount") == 2 &&
          (!PlayerChoices.acceptRequest || !PlayerChoices.introducedYourself)) {
            
            if (!PlayerChoices.acceptRequest) clockBlock = 201;
            if (!PlayerChoices.introducedYourself) clockBlock = 202;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Speech", LoadSceneMode.Additive);

        } else {
            clockBlock = 0;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TimeConfirm", LoadSceneMode.Additive);
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
