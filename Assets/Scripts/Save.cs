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
    // there's probably loops here we can use to save and load all this, right?

    static string[] array = {"PlantData.json", "GroceryData.json"};
    static string datapath;

    void Start() {
        datapath = Application.persistentDataPath;
    }

    public static void SaveData() {
        Debug.Log(datapath);

        string data = JsonUtility.ToJson(GetPlayerPlantChoices()); //there's an option to prettify (prettyPrint), default False
        System.IO.File.WriteAllText(datapath + "/PlantData.json", data);

        data = JsonUtility.ToJson(GetPlayerGroceryData());
        System.IO.File.WriteAllText(datapath + "/GroceryData.json", data);
    }

    public static void LoadData() {
        string filepath = System.IO.Path.Combine(datapath, "PlantData.json");
        string data = System.IO.File.ReadAllText(filepath);

        PlantData plantData = JsonUtility.FromJson<PlantData>(data);
        SetPlayerPlantChoices(plantData);

        filepath = System.IO.Path.Combine(datapath, "GroceryData.json");
        data = System.IO.File.ReadAllText(filepath);

        GroceryData grocData = JsonUtility.FromJson<GroceryData>(data);
        SetPlayerShopChoices(grocData);
    }

    public static void DeleteAllData() {
        foreach (string item in array) {
            string filepath = System.IO.Path.Combine(datapath, item);
            if (System.IO.File.Exists(filepath)) {
                System.IO.File.Delete(filepath);
            }
        }
    }

    public static PlantData GetPlayerPlantChoices() {
        PlantData data = new PlantData();
        data.buyPremiumPlant = PlayerChoices.buyPremiumPlant;
        data.receivePlantPromo  = PlayerChoices.receivePlantPromo;
        data.plantRibbonColor = PlayerChoices.plantRibbonColor;
        data.plantType = PlayerChoices.plantType;
        return data;
    }

    public static GroceryData GetPlayerGroceryData() {
        GroceryData data = new GroceryData();
        data.shoppedItems = PlayerChoices.shoppedItems;
        data.groceryDone = PlayerChoices.groceryDone;
        return data;
    }

    public static void SetPlayerPlantChoices(PlantData data) {
        PlayerChoices.buyPremiumPlant = data.buyPremiumPlant;
        PlayerChoices.receivePlantPromo = data.receivePlantPromo;
        PlayerChoices.plantRibbonColor = data.plantRibbonColor;
        PlayerChoices.plantType = data.plantType;
    }

    public static void SetPlayerShopChoices(GroceryData data) {
        PlayerChoices.shoppedItems = data.shoppedItems;
        PlayerChoices.groceryDone = data.groceryDone;
    }
}

[System.Serializable]
public class PlantData {
    public bool buyPremiumPlant;
    public bool receivePlantPromo;
    public string plantRibbonColor;
    public string plantType;
}

[System.Serializable]
public class GroceryData {
    public string[] shoppedItems;
    public bool groceryDone;
}
