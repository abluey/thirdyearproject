using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Temp : MonoBehaviour
{
    [SerializeField] private Button show;
    [SerializeField] private Button cheatBtn;

    private string message;

    // Start is called before the first frame update
    void Start()
    {
        show.onClick.AddListener(Show);
        cheatBtn.onClick.AddListener(Cheat);

        message = "PLANTS:"
                + "\nPlant Type: " + PlayerChoices.plantType
                + "\nPremium: " + PlayerChoices.buyPremiumPlant
                + "\nReceive promo: " + PlayerChoices.receivePlantPromo
                + "\nRibbon color: " + PlayerChoices.plantRibbonColor
                ;
    }

    private void Show() {
        Debug.Log(message);
    }

    private void Cheat() {
        int time = PlayerPrefs.GetInt("TimeCount");
        int day = PlayerPrefs.GetInt("DayCount");

        if (time < 2) {
            PlayerPrefs.SetInt("TimeCount", time + 1);
        } else {
            PlayerPrefs.SetInt("TimeCount", 0);
            PlayerPrefs.SetInt("DayCount", day + 1);
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
