using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentPage : MonoBehaviour
{
    [SerializeField] private Image presImage;
    [SerializeField] private Image shooImage;
    [SerializeField] private TMPro.TMP_Text boughtText;
    private string presName;

    [SerializeField] private Button buyBtn;

    void OnEnable() {
        presName = BirthdayManager.clickedName;
        boughtText.gameObject.SetActive(false);
        shooImage.gameObject.SetActive(false);
        presImage.gameObject.SetActive(false);

        if (PlayerChoices.buyPresent) {
            buyBtn.gameObject.SetActive(false);
        }

        switch (presName) {
            case "pres1":
                BirthdayManager.title.text = "Birth Day presents:\nCandy Hearts";
                break;
            case "pres2":
                BirthdayManager.title.text = "Birth Day presents:\nFresh Flowers";
                break;
            case "pres3":
                BirthdayManager.title.text = "Birth Day presents:\nFashionista Shoes";
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
