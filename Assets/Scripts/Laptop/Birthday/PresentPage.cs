using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentPage : MonoBehaviour
{
    [SerializeField] private Image presImage;
    [SerializeField] private TMPro.TMP_Text boughtText;
    private string presName;

    [SerializeField] private Button buyBtn;

    void OnEnable() {
        presName = BirthdayManager.clickedName;
        boughtText.gameObject.SetActive(false);

        if (PlayerChoices.buyPresent) {
            buyBtn.gameObject.SetActive(false);
        }

        switch (presName) {
            case "pres1":
                BirthdayManager.title.text = "Pres 1";
                break;
            case "pres2":
                BirthdayManager.title.text = "Pres 2";
                break;
            case "pres3":
                BirthdayManager.title.text = "Pres 3";
                break;
            default: Debug.Log("Something went wrong"); break;
        }

        presImage.sprite = Resources.Load<Sprite>($"Sprites/{presName}");
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
