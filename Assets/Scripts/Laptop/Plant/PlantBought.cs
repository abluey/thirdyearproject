using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlantBought : MonoBehaviour
{
    [SerializeField] private Canvas homepage;
    [SerializeField] private Canvas checkoutPage;
    [SerializeField] private Button backBtn;

    void Start()
    {
        backBtn.onClick.AddListener(Back);
    }

    private void Back() {
        StartCoroutine(WaitLoad());
    }

    private IEnumerator WaitLoad() {
        yield return new WaitForSeconds(0.2f);
        homepage.gameObject.SetActive(true);
        gameObject.SetActive(false);
        checkoutPage.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
