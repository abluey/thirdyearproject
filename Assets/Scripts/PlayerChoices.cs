using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoices : MonoBehaviour
{
    // so PlayerPrefs has:
    // TimeCount, DayCount
    // Name, DOB, Gender, Bio

    // plant
    public static bool buyPremiumPlant {get; set;} = true;  // default is to get it unless it's unchecked
    public static bool receivePlantPromo {get; set;} = true;  // default option is to receive checked promo
    public static string plantRibbonColor {get; set;} = "";
    public static string plantType {get; set;} = "";

    // grocery
    public static string[] shoppedItems {get; set;} = {}; // default empty? does this work?
    public static bool groceryDone {get; set;} = false;

    // choices are currently reset once you go back to MainMenu
    // choices will persist in JSON if saved
    public static void ResetChoices() {
        buyPremiumPlant = true;
        receivePlantPromo = true;
        plantRibbonColor = "";
        plantType = "";

        shoppedItems = Array.Empty<string>();   // creates an empty array
        groceryDone = false;
    }
}