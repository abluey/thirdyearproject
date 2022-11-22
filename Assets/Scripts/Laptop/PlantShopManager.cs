using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlantShopManager : MonoBehaviour
{   
    [SerializeField] private Image plantImage;
    [SerializeField] private TMPro.TMP_Text plantDesc;
    [SerializeField] private TMPro.TMP_Text plantDeets;

    private string plantImageName;
    private string plantDescText;
    private string plantDeetsText;

    void OnEnable() {
        switch (PlantManager.selectedPlant) {
            case "stabs":
                plantImageName = "plant_cactus";
                plantDescText = "Mr. Stabs: a cactus";
                plantDeetsText = "A hostile weapon? No, just a cactus! Mr. Stabs has no intention to hurt you, it just wants that sweet, sweet sunlight! On average, cacti require less care and water than other plants, so it's perfect for plant beginners, busy people, or just people who forget easily! Each Mr. Stab comes in its own premium pot and soil, which is not included in the advertised pricing. The spikes on a cactus can hurt, so be careful around children and pets!";
                break;
            case "juice":
                plantImageName = "plant_succ";
                plantDescText = "Juice Box: a succulent";
                plantDeetsText = "A succulent stores lots of water in its juicy, juicy leaves! Succulents are good plants for beginners who are just starting to get their green thumbs, although some Juice Boxes may be more temperamental than others. Warning: do not attempt to drink your Juice Box! It is still a plant, after all. Each Juice Box comes in its own premium pot and soil, which is not included in the advertised pricing. Take care not to over-water your Juice Box, or under-water it, either! Pet safe.";
                break;
            case "mb":
                plantImageName = "plant_money";
                plantDescText = "Baron Moneybags: a Chinese money plant";
                plantDeetsText = "Does money grow on trees? Maybe not a tree, but a small potted plant! A money plant is easy to take care of, and only needs watering once a week, but check that the soil is completely dry before you do so! Each Baron comes in its own premium pot and soil, which is not included in the advertised pricing. With a couple of Barons in your home, you're sure to attract copious amounts of luck and fortune! Not pet safe.";
                break;
            case "cheese":
                plantImageName = "plant_cheese";
                plantDescText = "Cheese: a Swiss cheese plant.";
                plantDeetsText = "Don't mistake this for the dairy product! You only need to water these beauties once a week, but ensure the soil is dry before you do so! The need to water can decrease during winter times, so be wary of over-watering your Cheeses! Each Cheese comes in its own premium pot and soil, which is not included in the advertised pricing. Cheeses grow quickly, so make sure they have lots of room to play! Not pet safe.";
                break;
            default: Debug.Log("PlantShopManager - OnEnabled"); break;
        }
        plantImage.sprite = Resources.Load<Sprite>($"Sprites/{plantImageName}");
        plantImage.SetNativeSize();
        plantImage.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        plantDesc.text = plantDescText;
        plantDeets.text = plantDeetsText;
    }

    void Start()
    {
        
    }
}
