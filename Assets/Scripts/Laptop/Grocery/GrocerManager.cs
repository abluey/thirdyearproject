using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GrocerManager : MonoBehaviour
{
    [SerializeField] private Button stapBtn;
    [SerializeField] private Button vegBtn;
    [SerializeField] private Button dairyBtn;
    [SerializeField] private Button condimentBtn;

    [SerializeField] private Button cartBtn;
    [SerializeField] private TMPro.TMP_Text cartText;
    [SerializeField] private Button backBtn;

    [SerializeField] private Canvas homepage;
    [SerializeField] private Canvas staples;
    [SerializeField] private Canvas veg;
    [SerializeField] private Canvas dairy;
    [SerializeField] private Canvas condiment;
    [SerializeField] private Canvas cartPage;
    [SerializeField] private Canvas purchasedPage;
    [SerializeField] private Canvas speech;

    private Canvas lastVisited;
    public static bool warning;

    // void OnEnable() {
    //     warning = false;
    // }

    void Start()
    {   
        warning = false;
        
        cartPage.gameObject.SetActive(false);
        purchasedPage.gameObject.SetActive(false);
        staples.gameObject.SetActive(false);
        veg.gameObject.SetActive(false);
        dairy.gameObject.SetActive(false);
        condiment.gameObject.SetActive(false);
        backBtn.gameObject.SetActive(false);
        speech.gameObject.SetActive(false);
        
        stapBtn.onClick.AddListener( delegate { LoadCanvas(staples); } );
        vegBtn.onClick.AddListener( delegate { LoadCanvas(veg); } );
        dairyBtn.onClick.AddListener( delegate { LoadCanvas(dairy); } );
        condimentBtn.onClick.AddListener( delegate { LoadCanvas(condiment); } );

        backBtn.onClick.AddListener(Homepage);
        cartBtn.onClick.AddListener(Cart);

        CartManager.cart.Clear();
    }

    private void LoadCanvas(Canvas canvas) {
        canvas.gameObject.SetActive(true);
        backBtn.gameObject.SetActive(true);
        homepage.gameObject.SetActive(false);
        lastVisited = canvas;
    }

    private void Homepage() {
        homepage.gameObject.SetActive(true);
        backBtn.gameObject.SetActive(false);
        lastVisited.gameObject.SetActive(false);
    }

    private void Cart() {
        cartPage.gameObject.SetActive(true);

        cartText.text = "";
        foreach (KeyValuePair<string, int> item in CartManager.cart) {
            // if (item.Value > 0) {
                cartText.text += "\n" + item.Value.ToString() + "x " + item.Key;
            // }
        }

        if (cartText.text == "") {
            cartText.text = "\nEmpty!";
            Debug.Log("No items");
        }
    }
}

