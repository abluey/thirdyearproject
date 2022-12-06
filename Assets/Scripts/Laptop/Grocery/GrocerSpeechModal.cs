using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrocerSpeechModal : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text thought;

    void OnEnable() {

        if (PlayerChoices.groceryDone) {
            // shopping done
            thought.text = "I've already done my shopping.";

        } else if (PlayerPrefs.GetInt("DayCount") >= 0 && PlayerPrefs.GetInt("TimeCount") != 1) {
            // not a shop day
            thought.text = "I don't need to shop right now.";

        } else if (CartManager.cart.Count == 0) {
            // empty cart
            thought.text = "I can't check out an empty cart!";

        } else if (!GrocerManager.warning) {
            // one checkout warning
            thought.text = "Is this everything I need? I don't want to have to make another order.";
            GrocerManager.warning = true;
        }
    }
}
