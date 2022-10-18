using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField] public Button triggerBtn;
    [SerializeField] public Button triggerBtn2;
    [SerializeField] public Button triggerBtn3;

    [SerializeField] private string loadScene;
    [SerializeField] private string loadScene2;
    [SerializeField] private string loadScene3;

    private string sceneToLoad;
    private int triggerBtnNum;
    public Animator animator;

    // Start is called before the first frame update and is only run once
    void Start() {
        triggerBtn.onClick.AddListener(delegate {FadeToScene(1);});

        // ? for checking for null type
        triggerBtn2?.onClick.AddListener(delegate {FadeToScene(2);});
        triggerBtn3?.onClick.AddListener(delegate {FadeToScene(3);});
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void FadeToScene(int triggerNumber) {
        triggerBtnNum = triggerNumber;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete() {
        switch (triggerBtnNum) {
            case 1:
                sceneToLoad = loadScene;
            break;
            case 2:
                if (!(loadScene2 == null)) sceneToLoad = loadScene2;
            break;
            case 3:
                if (!(loadScene3 == null)) sceneToLoad = loadScene3;
            break;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        asyncLoad.completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperation loadOperation) {
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}