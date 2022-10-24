using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KitchenManager : MonoBehaviour
{
    [SerializeField] private Button quitBtn;

    // Start is called before the first frame update
    void Start()
    {
        quitBtn.onClick.AddListener(Quit);
    }

    private void Quit() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Quit", LoadSceneMode.Additive);
    }
}
