using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTransition : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text content;
    [SerializeField] private TMPro.TMP_Text savedText;
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button endBtn;

    private string timeName;
    private int time;
    private int day;

    void Start()
    {   
        continueBtn.gameObject.SetActive(false);
        endBtn.gameObject.SetActive(false);
        savedText.text = "";

        time = PlayerPrefs.GetInt("TimeCount");
        day = PlayerPrefs.GetInt("DayCount");

        content.text = "Day " + day + ", " + GetTimeName(time);
        StartCoroutine(AdvanceTime());
    }

    private string GetTimeName(int time) {
        switch (time) {
            case 0: return "morning";
            case 1: return "afternoon";
            case 2: return "evening";
            default: return "ERROR: illegal time number";
        }
    }

    IEnumerator AdvanceTime() {
        yield return new WaitForSeconds(1.5f);

        if (time < 2) {
            PlayerPrefs.SetInt("TimeCount", time + 1);
        } else {
            PlayerPrefs.SetInt("DayCount", PlayerPrefs.GetInt("DayCount") + 1);
            PlayerPrefs.SetInt("TimeCount", 0);
        }
        PlayerPrefs.SetInt("CompletedTask", 0);

        content.text = "Day " + PlayerPrefs.GetInt("DayCount") + ", " + GetTimeName(PlayerPrefs.GetInt("TimeCount"));
        
        PlayerPrefs.Save();
        PlayerChoices.chatProgress = 0;
        Save.SaveData();
        savedText.text = "Game saved.";

        yield return new WaitForSeconds(1.5f);
        if (PlayerPrefs.GetInt("DayCount") == 3) {
            continueBtn.gameObject.SetActive(false);
            endBtn.gameObject.SetActive(true);
        } else {
            continueBtn.gameObject.SetActive(true);
            endBtn.gameObject.SetActive(false);
        }
        
    }
}
