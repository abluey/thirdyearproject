using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentPage : MonoBehaviour
{
    [SerializeField] private Image presImage;
    [SerializeField] private Image shooImage;
    [SerializeField] private TMPro.TMP_Text boughtText;
    [SerializeField] private TMPro.TMP_Text mainText;
    private string presName;

    [SerializeField] private Button buyBtn;

    void OnEnable() {
        presName = BirthdayManager.clickedName;
        boughtText.gameObject.SetActive(false);
        shooImage.gameObject.SetActive(false);
        presImage.gameObject.SetActive(false);

        if (PlayerChoices.buyPresent || !(PlayerPrefs.GetInt("DayCount") == 2 && PlayerPrefs.GetInt("TimeCount") == 1)) {
            buyBtn.gameObject.SetActive(false);
        }

        switch (presName) {
            case "pres1":
                BirthdayManager.title.text = "Birth Day presents:\nCandy Hearts";
                mainText.text = "A random bag of candied hearts, suitable not only for birthdays but every occasion!";
                break;
            case "pres2":
                BirthdayManager.title.text = "Birth Day presents:\nFresh Flowers";
                mainText.text = "Fresh, lively bouquets delivered next-day! The assortment is based on the date of your purchase to ensure the highest quality flowers.";
                break;
            case "pres3":
                BirthdayManager.title.text = "Birth Day presents:\nFashionista Shoes";
                mainText.text = "The latest shoes at your doorstep, delivery within 4 days guaranteed! Refunds and returns available if you're not happy with our current stock.";
                break;
            default: Debug.Log("Something went wrong"); break;
        }

        if (presName == "pres3") {
            shooImage.gameObject.SetActive(true);
        } else {
            presImage.sprite = Resources.Load<Sprite>($"Sprites/{presName}");
            presImage.gameObject.SetActive(true);
        }
        
    }

    void Start()
    {
        buyBtn.onClick.AddListener(Buy);
    }

    private void Buy() {
        buyBtn.gameObject.SetActive(false);
        boughtText.gameObject.SetActive(true);
        PlayerChoices.buyPresent = true;
    }
}
