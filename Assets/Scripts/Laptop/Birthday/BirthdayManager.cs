using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.SceneManagement;

public class BirthdayManager : MonoBehaviour
{
    [SerializeField] private Button adv1;
    [SerializeField] private Button adv2;
    [SerializeField] private Button adv3;
    [SerializeField] private Button adv4;
    [SerializeField] private Button adv5;
    [SerializeField] private Button quitt;

    [SerializeField] private Button pres1;
    [SerializeField] private Button pres2;
    [SerializeField] private Button pres3;

    void Start()
    {
        adv1.onClick.AddListener( delegate { LoadAd(1); });
        adv2.onClick.AddListener( delegate { LoadAd(2); });
        adv3.onClick.AddListener( delegate { LoadAd(3); });
        adv4.onClick.AddListener( delegate { LoadAd(4); });
        adv5.onClick.AddListener( delegate { LoadAd(5); });

        pres1.onClick.AddListener( delegate { LoadPres(1); });
        pres2.onClick.AddListener( delegate { LoadPres(2); });
        pres3.onClick.AddListener( delegate { LoadPres(3); });

        quitt.onClick.AddListener(Quitt);
    }

    private void LoadAd(int adNum) {

    }

    private void LoadPres(int presNum) {

    }

    private void Quitt() {

    }
}
