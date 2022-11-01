using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModalBack : MonoBehaviour
{
    [SerializeField] private Button backBtn;

    // Start is called before the first frame update
    void Start()
    {
        backBtn.onClick.AddListener(Back);
    }

    private void Back() {
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
