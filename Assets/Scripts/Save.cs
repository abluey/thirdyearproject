using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // so PlayerPrefs has:
    // TimeCount, DayCount
    // Name, DOB, Gender, Bio

public class Save : MonoBehaviour
{
    public void SaveData() {
        Debug.Log(Application.persistentDataPath);
        string data = JsonUtility.ToJson(new PlantData()); //there's an option to prettify (prettyPrint), default False
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PlantData.json", data);

    }
}

[System.Serializable]
public class PlantData {
    public bool buyPremiumPlant = PlayerChoices.buyPremiumPlant;
    public bool receivePlantPromo  = PlayerChoices.receivePlantPromo;
    public string plantRibbonColor = PlayerChoices.plantRibbonColor;
    public string plantType = PlayerChoices.plantType;
}
