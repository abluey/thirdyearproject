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

    private string actualPlantName;

    void OnEnable()
    {
        confirmBtn.onClick.AddListener(Confirm);

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
    }

    private void Confirm() {
        if (tcs.isOn) {
            PlayerChoices.receivePlantPromo = promo.isOn;
            PlayerChoices.buyPremiumPlant = PlantShopManager.plantPremium;
            PlayerChoices.plantRibbonColor = PlantShopManager.ribbonColor;
            PlayerChoices.plantType = PlantManager.selectedPlant;

            Debug.Log("Shopped");
        } else {
            Debug.Log("ya fucked it");
        }
    }
}
