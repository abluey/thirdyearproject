using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdPage : MonoBehaviour
{   
    [SerializeField] private Image adImage;
    [SerializeField] private TMPro.TMP_Text adText;
    private string adName;

    void OnEnable() {
        adName = BirthdayManager.adName;
        switch (adName) {
            case "ad1":
                // adText.text = "this is ad 1";
                BirthdayManager.title.text = "Ad 1";
                break;
            case "ad2":
                // adText.text = "this is ad 2";
                BirthdayManager.title.text = "Ad 2";
                break;
            case "ad3":
                // adText.text = "this is ad 3";
                BirthdayManager.title.text = "Ad 3";
                break;
            case "ad5":
                // adText.text = "this is ad 5";
                BirthdayManager.title.text = "Ad 5";
                break;
            case "quitt":
                // adText.text = "this is quitt";
                BirthdayManager.title.text = "Quitt";
                break;
            default: Debug.Log("Something went wrong"); break;
        }

        adImage.sprite = Resources.Load<Sprite>($"Sprites/{adName}");
    }

    void Start()
    {
        
    }
}
