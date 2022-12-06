using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlantCheckout : MonoBehaviour
{
    [SerializeField] private Button confirmBtn;
    [SerializeField] private Toggle tcs;
    [SerializeField] private Toggle promo;

    [SerializeField] private TMPro.TMP_Text plantName;
    [SerializeField] private TMPro.TMP_Text price;
    [SerializeField] private TMPro.TMP_Text errorText;

    [SerializeField] private Canvas purchasedPage;
    [SerializeField] private Canvas speech;

    private string actualPlantName;

    void Awake() {
        confirmBtn.onClick.AddListener(Confirm);
    }
    
    void OnEnable()
    {
        switch (PlantManager.selectedPlant) {
            case "stabs": actualPlantName = "Mr. Stabs"; break;
            case "mb": actualPlantName = "Baron Moneybags"; break;
            case "juice": actualPlantName = "Juice Box"; break;
            case "cheese": actualPlantName = "Cheese"; break;
            default: actualPlantName = "Error 404: not found"; break;
        }

        if (PlantShopManager.plantPremium) {
            plantName.text = actualPlantName + " - Premium";
            price.text = "Total Price: £19.99";
        } else {
            plantName.text = actualPlantName;
            price.text = "Total Price: £4.99";
        }

        errorText.gameObject.SetActive(false);
    }

    private void Confirm() {

        // can only buy a plant when the buy plant activity is active on ToDo list
        if (PlayerPrefs.GetInt("DayCount") == 1 && PlayerPrefs.GetInt("TimeCount") >= 1) {

            // no saved record of bought plant yet and has checked the Ts&Cs box
            if (PlayerChoices.plantType == "" && tcs.isOn) {
                errorText.gameObject.SetActive(false);
                
                // setting record
                PlayerChoices.receivePlantPromo = promo.isOn;
                PlayerChoices.buyPremiumPlant = PlantShopManager.plantPremium;
                PlayerChoices.plantRibbonColor = PlantShopManager.ribbonColor;
                PlayerChoices.plantType = PlantManager.selectedPlant;

                purchasedPage.gameObject.SetActive(true);

            } else if (PlayerChoices.plantType == "" && !tcs.isOn) {
                // no T&Cs box ticked
                errorText.gameObject.SetActive(true);

            } else {
                // if the Player's plant type has already been set; i.e. already bought a plant
                speech.gameObject.SetActive(true);
            }
        } else {
            speech.gameObject.SetActive(true);
        }
    }
}
