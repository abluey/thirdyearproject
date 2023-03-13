using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModalBack : MonoBehaviour
{
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
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
