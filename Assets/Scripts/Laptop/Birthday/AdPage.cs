using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdPage : MonoBehaviour
{
    private string adName;

    void OnEnable() {
        adName = BirthdayManager.adName;
        switch (adName) {
            case "ad1": break;
            case "ad2": break;
            case "ad3": break;
            case "ad5": break;
            default: Debug.Log("Something went wrong"); break;
        }
    }

    void Start()
    {
        
    }
}
