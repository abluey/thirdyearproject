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

}
