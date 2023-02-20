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
                adText.text = "this is ad 1";
                break;
            case "ad2":
                adText.text = "this is ad 2";
                break;
            case "ad3":
                adText.text = "this is ad 3";
                break;
            case "ad5":
                adText.text = "this is ad 5";
                break;
            case "quitt":
                adText.text = "this is quitt";
                break;
            default: Debug.Log("Something went wrong"); break;
        }

        adImage.sprite = Resources.Load<Sprite>($"Sprites/{adName}");
    }

    void Start()
    {
        
    }
}
