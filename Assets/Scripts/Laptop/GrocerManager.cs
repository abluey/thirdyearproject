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

    [SerializeField] private Button backBtn;

    [SerializeField] private Canvas homepage;
    [SerializeField] private Canvas staples;
    [SerializeField] private Canvas veg;
    [SerializeField] private Canvas dairy;
    [SerializeField] private Canvas condiment;

    private Canvas lastVisited;

    void Start()
    {
        stapBtn.onClick.AddListener( delegate { LoadCanvas(staples); } );
        vegBtn.onClick.AddListener( delegate { LoadCanvas(veg); } );
        dairyBtn.onClick.AddListener( delegate { LoadCanvas(dairy); } );
        condimentBtn.onClick.AddListener( delegate { LoadCanvas(condiment); } );

        backBtn.onClick.AddListener(Homepage);

        staples.gameObject.SetActive(false);
        veg.gameObject.SetActive(false);
        dairy.gameObject.SetActive(false);
        condiment.gameObject.SetActive(false);
        backBtn.gameObject.SetActive(false);
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
}

