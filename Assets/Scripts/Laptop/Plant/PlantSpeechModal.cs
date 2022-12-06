using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantSpeechModal : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text thought;

    void OnEnable() {
        if (PlayerChoices.plantType == "") {
            thought.text = "I don't need a plant right now!";
        } else {
            thought.text = "I've already bought a plant!";
        }
    }
}
