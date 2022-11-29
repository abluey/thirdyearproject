using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaplesManager : MonoBehaviour
{
   [SerializeField] private Button addBread;
   [SerializeField] private Button subBread;
   [SerializeField] private Button addPasta;
   [SerializeField] private Button subPasta;

   [SerializeField] private TMPro.TMP_Text breadCount;
   [SerializeField] private TMPro.TMP_Text pastaCount;

   private int FCOI;    // "food count of interest"

    void Start()
    {
        // cart manager
        addBread.onClick.AddListener(delegate { CartManager.AddFood("Bread");} );
        subBread.onClick.AddListener(delegate { CartManager.SubtractFood("Bread"); });
        addPasta.onClick.AddListener(delegate { CartManager.AddFood("Pasta");} );
        subPasta.onClick.AddListener(delegate { CartManager.SubtractFood("Pasta"); });

        // visual updates on the canvas
        addBread.onClick.AddListener(delegate { AddCount("Bread"); });
        addPasta.onClick.AddListener(delegate { AddCount("Pasta"); });
        subBread.onClick.AddListener(delegate { SubCount("Bread"); });
        subPasta.onClick.AddListener(delegate { SubCount("Pasta"); });

        subBread.gameObject.SetActive(false);
        subPasta.gameObject.SetActive(false);
    }

    private void AddCount(string food) {
        // can't rely on counting using the CartManager because no idea which code gets executed first

        switch (food) {
            case "Bread": 
                breadCount.text = (Int32.Parse(breadCount.text) + 1).ToString();
                if (breadCount.text == "1") subBread.gameObject.SetActive(true); // only sets true once
                break;
            case "Pasta":
                pastaCount.text = (Int32.Parse(pastaCount.text) + 1).ToString();
                if (pastaCount.text == "1") subPasta.gameObject.SetActive(true);
                break;
            default: Debug.Log("Something went wrong"); break;
        }
    }

    private void SubCount(string food) {
        switch (food) {
            case "Bread": 
                breadCount.text = (Int32.Parse(breadCount.text) - 1).ToString();
                if (breadCount.text == "0") subBread.gameObject.SetActive(false); // only sets false once
                break;
            case "Pasta":
                pastaCount.text = (Int32.Parse(pastaCount.text) - 1).ToString();
                if (pastaCount.text == "0") subPasta.gameObject.SetActive(false);
                break;
            default: Debug.Log("Something went wrong"); break;
        }
    }
}
