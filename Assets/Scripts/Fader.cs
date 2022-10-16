using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField] public Button triggerBtn;
    [SerializeField] private string sceneToLoad;
    public Animator animator;

    // Start is called before the first frame update and is only run once
    void Start() {
        triggerBtn.onClick.AddListener(FadeToScene);
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void FadeToScene() {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        asyncLoad.completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperation loadOperation) {
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}