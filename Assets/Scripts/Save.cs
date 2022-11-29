using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // so PlayerPrefs has:
    // TimeCount, DayCount - we are NOT moving these to JSON because they change so quickly
    // Name, DOB, Gender, Bio

    // logic: only needs saving into JSON if it's not player-facing because so far i'm lazy

    // places to save: GameMenu (Save button), TimeTransition.AdvanceTime

public class Save : MonoBehaviour
{
    public static void SaveData() {
        Debug.Log(Application.persistentDataPath);
        string data = JsonUtility.ToJson(GetPlayerPlantChoices()); //there's an option to prettify (prettyPrint), default False
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PlantData.json", data);
    }

    public void LoadData() {
        string filepath = System.IO.Path.Combine(Application.persistentDataPath, "PlantData.json");
        string data = System.IO.File.ReadAllText(filepath);

        PlantData plantData = JsonUtility.FromJson<PlantData>(data);
        SetPlayerPlantChoices(plantData);
    }

    public static PlantData GetPlayerPlantChoices() {
        PlantData data = new PlantData();
        data.buyPremiumPlant = PlayerChoices.buyPremiumPlant;
        data.receivePlantPromo  = PlayerChoices.receivePlantPromo;
        data.plantRibbonColor = PlayerChoices.plantRibbonColor;
        data.plantType = PlayerChoices.plantType;
        return data;
    }

    public void SetPlayerPlantChoices(PlantData data) {
        PlayerChoices.buyPremiumPlant = data.buyPremiumPlant;
        PlayerChoices.receivePlantPromo = data.receivePlantPromo;
        PlayerChoices.plantRibbonColor = data.plantRibbonColor;
        PlayerChoices.plantType = data.plantType;
    }
}

[System.Serializable]
public class PlantData {
    public bool buyPremiumPlant;
    public bool receivePlantPromo;
    public string plantRibbonColor;
    public string plantType;
}
