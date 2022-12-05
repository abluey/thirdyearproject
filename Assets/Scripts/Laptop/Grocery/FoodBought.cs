using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FoodBought : MonoBehaviour
{
    // save happens in FoodManager when you click confirm checkout
    // this script tidies up the canvases and returns the user to the home grocer page

    [SerializeField] private Canvas homepage;
    [SerializeField] private Canvas staples;
    [SerializeField] private Canvas veg;
    [SerializeField] private Canvas dairy;
    [SerializeField] private Canvas condiment;

    [SerializeField] private Button homeBtn;

    void Start()
    {
        homeBtn.onClick.AddListener(Home);
    }

    private void Home() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        homepage.gameObject.SetActive(true);
        gameObject.SetActive(false);
        staples.gameObject.SetActive(false);
        veg.gameObject.SetActive(false);
        dairy.gameObject.SetActive(false);
        condiment.gameObject.SetActive(false);
    }
}
