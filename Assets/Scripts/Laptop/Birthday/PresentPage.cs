using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentPage : MonoBehaviour
{
    [SerializeField] private Image presImage;
    // [SerializeField] private TMPro.TMP_Text presText;
    private string presName;

    [SerializeField] private Button buyBtn;

    void OnEnable() {
        presName = BirthdayManager.presName;
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
        // backBtn.onClick.AddListener(Back);
        buyBtn.onClick.AddListener(Buy);
    }

    private void Buy() {

    }
}
