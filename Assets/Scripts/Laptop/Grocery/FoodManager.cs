using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodManager : MonoBehaviour
{
    [SerializeField] private Button add1;
    [SerializeField] private Button sub1;
    [SerializeField] private string food1;

    [SerializeField] private Button add2;
    [SerializeField] private Button sub2;
    [SerializeField] private string food2;

    [SerializeField] private Button add3;
    [SerializeField] private Button sub3;
    [SerializeField] private string food3;

    [SerializeField] private TMPro.TMP_Text foodCount1;
    [SerializeField] private TMPro.TMP_Text foodCount2;
    [SerializeField] private TMPro.TMP_Text foodCount3;

    void Start()
    {
        // cart manager
        add1.onClick.AddListener(delegate { CartManager.AddFood(food1);} );
        sub1.onClick.AddListener(delegate { CartManager.SubtractFood(food1); });
        add2.onClick.AddListener(delegate { CartManager.AddFood(food2);} );
        sub2.onClick.AddListener(delegate { CartManager.SubtractFood(food2); });
        // guaranteed two options at least, so third is not guaranteed so check for null
        add3?.onClick.AddListener(delegate { CartManager.AddFood(food3);} );
        sub3?.onClick.AddListener(delegate { CartManager.SubtractFood(food3); });

        // visual updates on the canvas
        add1.onClick.AddListener(delegate { ModCount1(1); });
        add2.onClick.AddListener(delegate { ModCount2(1); });
        add3?.onClick.AddListener(delegate { ModCount3(1); });

        sub1.onClick.AddListener(delegate { ModCount1(0); });
        sub2.onClick.AddListener(delegate { ModCount2(0); });
        sub3?.onClick.AddListener(delegate { ModCount3(0); });

        sub1.gameObject.SetActive(false);
        sub2.gameObject.SetActive(false);
        sub3?.gameObject.SetActive(false);
    }

    private void ModCount1(int mod) {
        // 0 means to subtract
        if (mod == 0) {
            foodCount1.text = (Int32.Parse(foodCount1.text) - 1).ToString();
            if (foodCount1.text == "0") sub1.gameObject.SetActive(false);
        }
        // 1 means to add
        else {
            foodCount1.text = (Int32.Parse(foodCount1.text) + 1).ToString();
            if (foodCount1.text == "1") sub1.gameObject.SetActive(true);    // only sets true once
        }
    }

    private void ModCount2(int mod) {
        // 0 means to subtract
        if (mod == 0) {
            foodCount2.text = (Int32.Parse(foodCount2.text) - 1).ToString();
            if (foodCount2.text == "0") sub2.gameObject.SetActive(false);
        }
        // 1 means to add
        else {
            foodCount2.text = (Int32.Parse(foodCount2.text) + 1).ToString();
            if (foodCount2.text == "1") sub2.gameObject.SetActive(true);    // only sets true once
        }
    }

    private void ModCount3(int mod) {
        // 0 means to subtract
        if (mod == 0) {
            foodCount3.text = (Int32.Parse(foodCount3?.text) - 1).ToString();
            if (foodCount3?.text == "0") sub3?.gameObject.SetActive(false);
        }
        // 1 means to add
        else {
            foodCount3.text = (Int32.Parse(foodCount3?.text) + 1).ToString();
            if (foodCount3?.text == "1") sub3?.gameObject.SetActive(true);    // only sets true once
        }
    }
}
