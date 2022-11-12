using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutManager : MonoBehaviour
{
    [SerializeField] private Button homeBtn;

    [SerializeField] private Canvas homepage;

    // Start is called before the first frame update
    void Start()
    {
        homeBtn.onClick.AddListener(Homepage);
    }

    private void Homepage() {
        homepage.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
