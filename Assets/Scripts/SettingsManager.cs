using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Button backBtn;

    // Start is called before the first frame update
    void Start()
    {
        backBtn.onClick.AddListener(Back);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Back() {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
