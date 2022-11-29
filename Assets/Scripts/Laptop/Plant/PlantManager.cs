using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlantManager : MonoBehaviour
{
    [SerializeField] private Button stabsBtn;
    [SerializeField] private Button juiceBtn;
    [SerializeField] private Button mbBtn;
    [SerializeField] private Button cheeseBtn;

    [SerializeField] private Button backBtn;
    [SerializeField] private Button tcBtn;

    [SerializeField] private Canvas homepage;
    [SerializeField] private Canvas shopPage;
    [SerializeField] private Canvas checkoutPage;
    [SerializeField] private Canvas purchasedPage;
    [SerializeField] private Canvas tcsPage;
    public static string selectedPlant;

    void Start()
    {
        stabsBtn.onClick.AddListener( delegate { LoadCanvas("stabs"); } );
        juiceBtn.onClick.AddListener( delegate { LoadCanvas("juice"); } );
        mbBtn.onClick.AddListener( delegate { LoadCanvas("mb"); } );
        cheeseBtn.onClick.AddListener( delegate { LoadCanvas("cheese"); } );

        backBtn.onClick.AddListener(Homepage);
        tcBtn.onClick.AddListener(TCs);

        tcsPage.gameObject.SetActive(false);
        shopPage.gameObject.SetActive(false);
        checkoutPage.gameObject.SetActive(false);
        purchasedPage.gameObject.SetActive(false);
        backBtn.gameObject.SetActive(false);
    }

    private void LoadCanvas(string selected) {
        selectedPlant = selected;
        shopPage.gameObject.SetActive(true);
        backBtn.gameObject.SetActive(true);
        homepage.gameObject.SetActive(false);
    }

    private void Homepage() {
        selectedPlant = "";
        homepage.gameObject.SetActive(true);
        backBtn.gameObject.SetActive(false);
        shopPage.gameObject.SetActive(false);
    }

    private void TCs() {
        tcsPage.gameObject.SetActive(true);
    }
}
