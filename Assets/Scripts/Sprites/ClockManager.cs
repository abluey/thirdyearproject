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
        /***
        DAY 0
        ***/

        // no profile set
        if (!PlayerPrefs.HasKey("Name")) {
            clockBlock = 100;
        }

        if (PlayerPrefs.GetInt("DayCount") == 0) {
            
            // shopping not done
            if (PlayerPrefs.GetInt("TimeCount") == 1 && !PlayerChoices.groceryDone) {
                clockBlock = 101;
            }

            // FriendMi chat not finished
            else if (PlayerPrefs.GetInt("TimeCount") == 2 &&
                (!PlayerChoices.acceptRequest ||
                !PlayerChoices.introducedYourself ||
                PlayerChoices.chatProgress != 1)) {
                    if (!PlayerChoices.acceptRequest) clockBlock = 102;
                    else if (!PlayerChoices.introducedYourself) clockBlock = 202;
                    else if (PlayerChoices.chatProgress != 1) clockBlock = 5;
                }
        }

        /***
        DAY 1
        ***/

        else if (PlayerPrefs.GetInt("DayCount") == 1) {

            // haven't finished chat on either time 0 or 1
            if ((PlayerPrefs.GetInt("TimeCount") == 0 || PlayerPrefs.GetInt("TimeCount") == 1) && PlayerChoices.chatProgress != 1) {
                clockBlock = 5;
            }

            // plant shenanigans
            else if (PlayerPrefs.GetInt("TimeCount") == 2) {
                
                if (PlayerChoices.plantType == "") {
                    clockBlock = 111;
                }
                else if (PlayerChoices.chatProgress != 1) {
                    clockBlock = 5;
                }
            }
        }

        /***
        DAY 2
        ***/

        else if (PlayerPrefs.GetInt("DayCount") == 2) {

            if (PlayerPrefs.GetInt("TimeCount") == 1) {
                if (!PlayerChoices.buyPresent) {
                    clockBlock = 121;
                }
                else if (PlayerChoices.chatProgress != 1) {
                    clockBlock = 5;
                }
            }
            
            // ELEPHANT should encompass chat not complete in times 0 and 2
            else {
                if (PlayerChoices.chatProgress != 1) {
                    clockBlock = 5;
                }
            }
        }

        // everything ok
        if (clockBlock == 0) {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TimeConfirm", LoadSceneMode.Additive);
        } else {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SpeechModal", LoadSceneMode.Additive);
        }
        
    }
}
