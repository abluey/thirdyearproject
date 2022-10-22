using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField] private Button backBtn;

    // Start is called before the first frame update
    void Start()
    {
        backBtn.onClick.AddListener(Back);
    }

    private void Back() {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
