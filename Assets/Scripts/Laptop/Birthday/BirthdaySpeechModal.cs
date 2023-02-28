using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirthdaySpeechModal : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text thought;

    void OnEnable() {
        if (PlayerChoices.buyPresent) {
            thought.text = "I've already bought a present.";
        } else {
            thought.text = "I don't need to buy anything right now.";
        }
    }
}
