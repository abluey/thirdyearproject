using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FMManager : MonoBehaviour
{
    [SerializeField] private Button friendsBtn;
    [SerializeField] private Button profileBtn;
    [SerializeField] private Button aboutBtn;

    [SerializeField] private Canvas homepage;
    [SerializeField] private Canvas friends;
    [SerializeField] private Canvas profile;
    [SerializeField] private Canvas about;

    // Start is called before the first frame update
    void Start()
    {
        friendsBtn.onClick.AddListener(Friends);
        profileBtn.onClick.AddListener(Profile);
        aboutBtn.onClick.AddListener(About);

        friends.gameObject.SetActive(false);
        profile.gameObject.SetActive(false);
        about.gameObject.SetActive(false);
    }

    private void Friends() {
        friends.gameObject.SetActive(true);
        homepage.gameObject.SetActive(false);
    }

    private void Profile() {
        profile.gameObject.SetActive(true);
        homepage.gameObject.SetActive(false);
    }

    private void About() {
        about.gameObject.SetActive(true);
        homepage.gameObject.SetActive(false);
    }

}
