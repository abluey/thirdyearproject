using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTransition : MonoBehaviour
{
    public TMPro.TMP_Text content;
    public TMPro.TMP_Text savedText;
    public Button continueBtn;

    private string timeName;
    private int time;
    private int day;

    // Start is called before the first frame update
    void Start()
    {   
        continueBtn.gameObject.SetActive(false);
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
        content.text = "Day " + PlayerPrefs.GetInt("DayCount") + ", " + GetTimeName(PlayerPrefs.GetInt("TimeCount"));
        PlayerPrefs.Save();
        savedText.text = "Game saved.";

        yield return new WaitForSeconds(1.5f);
        continueBtn.gameObject.SetActive(true);
    }
}
