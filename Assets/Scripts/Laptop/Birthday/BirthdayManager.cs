using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirthdayManager : MonoBehaviour
{
    [SerializeField] private Button adv1;
    [SerializeField] private Button adv2;
    [SerializeField] private Button adv3;
    [SerializeField] private Button adv4;
    [SerializeField] private Button adv5;
    [SerializeField] private Button quittBtn;

    [SerializeField] private Button pres1;
    [SerializeField] private Button pres2;
    [SerializeField] private Button pres3;

    [SerializeField] private Canvas adPage;
    [SerializeField] private Canvas Advert4;
    [SerializeField] private Canvas presentPage;
    [HideInInspector] public static string adName;
    private Canvas lastVisited;

    [SerializeField] private Canvas homepage;
    [SerializeField] private Button homeBtn;
    [SerializeField] private Button quitBtn;

    void Start()
    {
        adv1.onClick.AddListener( delegate { LoadCanvas(adPage, "ad1"); });
        adv2.onClick.AddListener( delegate { LoadCanvas(adPage, "ad2"); });
        adv3.onClick.AddListener( delegate { LoadCanvas(adPage, "ad3"); });
        adv4.onClick.AddListener( delegate { LoadCanvas(Advert4, "ad4"); });
        adv5.onClick.AddListener( delegate { LoadCanvas(adPage, "ad5"); });

        pres1.onClick.AddListener( delegate { LoadCanvas(presentPage, "pres1"); });
        pres2.onClick.AddListener( delegate { LoadCanvas(presentPage, "pres2"); });
        pres3.onClick.AddListener( delegate { LoadCanvas(presentPage, "pres3"); });

        quittBtn.onClick.AddListener( delegate { LoadCanvas(adPage, "quitt"); });

        homeBtn.onClick.AddListener(Home);
    }

    private void LoadCanvas(Canvas canvas, string canName) {
        if (canName[0] == 'a') {
            PlayerChoices.virusNum += 1;
        }
        adName = canName;
        lastVisited = canvas;
        canvas.gameObject.SetActive(true);
        homepage.gameObject.SetActive(false);
        homeBtn.gameObject.SetActive(true);
        quitBtn.gameObject.SetActive(false);
    }

    private void Home() {
        homepage.gameObject.SetActive(true);
        lastVisited.gameObject.SetActive(false);
        homeBtn.gameObject.SetActive(false);
        quitBtn.gameObject.SetActive(true);
    }
}
