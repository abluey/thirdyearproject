using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelpManager : MonoBehaviour
{   
    public void Back() {
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
