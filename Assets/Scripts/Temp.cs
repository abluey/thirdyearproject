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
    private string message2;

    // Start is called before the first frame update
    void Start()
    {
        show.onClick.AddListener(Show);
        cheatBtn.onClick.AddListener(Cheat);
    }

    private void Show() {
        message = "PLANTS:"
                + "\nPlant Type: " + PlayerChoices.plantType
                + "\nPremium: " + PlayerChoices.buyPremiumPlant
                + "\nReceive promo: " + PlayerChoices.receivePlantPromo
                + "\nRibbon color: " + PlayerChoices.plantRibbonColor
                ;

        string temp = "";

        foreach(string item in PlayerChoices.shoppedItems)
        {
            temp += item + " ";
        }  

        message2 = "GROC:"
                + "\nCart: " + temp
                + "\nHasShoppped: " + PlayerChoices.groceryDone
                ;
        Debug.Log(message);
        Debug.Log(message2);
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
        PlayerChoices.chatProgress = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
